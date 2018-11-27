using System;
using Data;
using Entities;
using CustomizeException;

namespace BussinesClass
{
    /// <summary>
    /// Class to manage the UserSession entity and db table
    /// </summary>
    /// <returns></returns>
    public class UserSessionBusiness
    {
        private UserSessionDataClass userSesionManager = null;
        public UserSessionBusiness()
        {
            userSesionManager = new UserSessionDataClass();
        }

        public void  SetLoginUserSession(int userId, string conecctionId, string username)
        {
            UserSessions newUserSession = new UserSessions();
            newUserSession.LoginDate = DateTime.Now;
            newUserSession.UserId = userId;
            newUserSession.ConnectionId = conecctionId;
            newUserSession.UserName = username;
            userSesionManager.SetUserSeccion(newUserSession);

        }

        public UserSessions GetUserSession(string conectionId)
        {
            return userSesionManager.GetUserByConectionId(conectionId);
        }
    }
}
