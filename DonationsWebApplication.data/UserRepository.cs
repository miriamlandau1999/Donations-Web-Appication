using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationsWebApplication.data
{
    public class UserRepository
    {
        private string _ConnectionString;

        public UserRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public void Add(User user, string password)
        {
            user.Salt = PasswordHelper.GenerateSalt();
            user.PasswordHash = PasswordHelper.HashPassword(password, user.Salt);
            using(var context = new DonationsDataDataContext(_ConnectionString))
            {
                context.Users.InsertOnSubmit(user);
                context.SubmitChanges();
            }
        }
       
        public bool IsMatch(string password, string email)
        {
            using (var context = new DonationsDataDataContext(_ConnectionString))
            {
                User user = context.Users.FirstOrDefault(u => u.Email == email);
                if(user != null)
                {
                    string salt = user.Salt;
                    string passwordHash = user.PasswordHash;
                    return PasswordHelper.PasswordMatch(password, salt, passwordHash);
                }
                return false;
            }
        }
        public User GetByUserName(string email)
        {
            
            using (var context = new DonationsDataDataContext(_ConnectionString))
            {
                return context.Users.FirstOrDefault(u => u.Email == email);
            }
        }
    }
   
}
