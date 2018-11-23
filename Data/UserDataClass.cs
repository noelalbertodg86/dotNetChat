using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Data
{
    public class UserDataClass
    {
        private ChatDBEntities entities = null;
        public UserDataClass()
        {
            entities = new ChatDBEntities();

        }

        public User GetUser(string user, string password)
        {
            return entities.Users.Where(p => p.UserName == user && p.Password == password).FirstOrDefault();
        }

        public User GetUser(string user)
        {
            return entities.Users.Where(p => p.UserName == user ).FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            entities.Users.Add(user);
            entities.SaveChanges();
        }
    }
}
