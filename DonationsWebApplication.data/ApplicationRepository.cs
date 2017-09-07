using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationsWebApplication.data
{
    public class ApplicationRepository
    {
        private string _ConnectionString;

        public ApplicationRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public void Add(Application application)
        {
            application.Date = DateTime.Now;
            using (var context = new DonationsDataDataContext(_ConnectionString))
            {
                context.Applications.InsertOnSubmit(application);
                context.SubmitChanges();
            }
        }
        public IEnumerable<Application> GetApplications()
        {
            using (var context = new DonationsDataDataContext(_ConnectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Application>(a => a.Category);
                loadOptions.LoadWith<Application>(a => a.User);
                context.LoadOptions = loadOptions;
                return context.Applications.ToList();
            }
        }
        public IEnumerable<Application> GetApplications(int UserId)
        {
            return GetApplications().Where(a => a.UserId == UserId);
        }
        public IEnumerable<Application> GetPending()
        {
            return GetApplications().Where(a => a.Approved == null);
        }
        public IEnumerable<Application> GetPending(int? CategoryId)
        {
            return CategoryId == null ? GetPending(): GetApplications().Where(a => a.Approved == null && a.CategoryId == CategoryId);
        }

        public void AplicationAction(int ApplicationId, bool IsAccepted)
        {
            using(var context = new DonationsDataDataContext(_ConnectionString))
            {
                context.Applications.FirstOrDefault(a => a.Id == ApplicationId).Approved = IsAccepted;
                context.SubmitChanges();
            }
        }
    }
}
