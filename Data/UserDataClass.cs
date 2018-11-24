using System.Linq;
using Entities;

namespace Data
{
    /// <summary>
    /// User LINQ manager 
    /// </summary>
    /// <returns></returns>
    public class UserDataClass
    {
        private ChatDBEntities entities = null;
        public UserDataClass()
        {
            entities = new ChatDBEntities();

        }

        //get user by user and password for login
        public User GetUser(string user, string password)
        {
            return entities.Users.Where(p => p.UserName == user).ToList().Where(q=>q.Password == password).FirstOrDefault();
        }

        //get user by user name
        public User GetUser(string user)
        {
            return entities.Users.Where(p => p.UserName == user ).FirstOrDefault();
        }

        //create user
        public void CreateUser(User user)
        {
            entities.Users.Add(user);
            entities.SaveChanges();
        }
    }
}
