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
        private ChatDBContext entities = null;
        public UserSessionDataClass()
        {
            entities = new ChatDBContext();

        }

        public void SetUserSeccion(UserSessions userSessionEntities)
        {
             entities.UserSessions.Add(userSessionEntities);

        }

        public Entities.UserSessions GetUserByConectionId(string conectionId)
        {
            return entities.UserSessions.Where(p => p.ConnectionId == conectionId).FirstOrDefault();
        }

        public Entities.UserSessions GetUserCurrentConectionId(int userId)
        {
            return entities.UserSessions.Where(p => p.UserId == userId).ToList().Where(q=>q.LogoutDate == null).OrderByDescending(w=>w.LoginDate).FirstOrDefault();
        }


    }
}
