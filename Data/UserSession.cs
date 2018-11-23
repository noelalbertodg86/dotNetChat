using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Data
{
    public class UserSession
    {
        private ChatDBEntities entities = null;
        public UserSession()
        {
            entities = new ChatDBEntities();

        }

        public void SetUserSeccion(Entities.UserSession userSessionEntities)
        {
             entities.UserSessions.Add(userSessionEntities);

        }

        public Entities.UserSession IsUserLogin(int userId)
        {
            return entities.UserSessions.Where(p => p.UserID == userId && p.LogoutDate == null).FirstOrDefault();
        }


    }
}
