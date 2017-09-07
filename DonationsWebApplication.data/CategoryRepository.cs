using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationsWebApplication.data
{
    public class CategoryRepository
    {
        private string _ConnectionString;

        public CategoryRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }
        public IEnumerable<Category> GetCategories()
        {
            using (var context = new DonationsDataDataContext(_ConnectionString))
            {
                return context.Categories.ToList();
            }
                
        }
    }
}
