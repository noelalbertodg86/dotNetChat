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

        public void  SetLoginUserSession(int userId, string conecctionId)
        {
            UserSession newUserSession = new UserSession();
            newUserSession.LoginDate = DateTime.Now;
            newUserSession.UserID = userId;
            newUserSession.ConnectionId = conecctionId;
            userSesionManager.SetUserSeccion(newUserSession);

        }

        public UserSession GetUserSession(string conectionId)
        {
            return userSesionManager.GetUserByConectionId(conectionId);
        }
    }
}
