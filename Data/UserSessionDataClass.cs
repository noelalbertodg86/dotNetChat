using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Data
{
    /// <summary>
    /// UserSession LINQ manager 
    /// </summary>
    /// <returns></returns>
    public class UserSessionDataClass
    {
        private ChatDBEntities entities = null;
        public UserSessionDataClass()
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

        public Entities.UserSession GetUserByConectionId(string conectionId)
        {
            return entities.UserSessions.Where(p => p.ConnectionId == conectionId && p.LogoutDate == null).FirstOrDefault();
        }


    }
}
