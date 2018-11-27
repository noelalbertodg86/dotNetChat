using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using BussinesClass;
using Entities;


namespace Server
{
    /// <summary>
    /// Gets or sets an object that can be used to invoke methods on the clients connected to this hub.
    /// Context Context Context. Gets or sets the hub caller context.
    /// </summary>
    /// <returns></returns>
    public class ChatHub : Hub
    {

        /// <summary>
        /// send to client method broadcastMessage the parameters user and text
        /// </summary>
        /// <returns></returns>
        public void SendMessage(string user, string text)
        {
            Clients.Client(Context.ConnectionId).SendAsync("broadcastMessage", user, text);
        }

        /// <summary>
        /// Gets de connection id of online user, each conected user has their own id
        /// </summary>
        /// <returns></returns>
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        /// <summary>
        ///  when a user is disconnected, other users are notified
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            string disconectedUserName = "";
            UserSessionBusiness userSessionBusiness = new UserSessionBusiness();
            Entities.UserSessions userSession = userSessionBusiness.GetUserSession(Context.ConnectionId);
            if (userSession != null)
            { disconectedUserName = userSession.UserName; }

            Clients.All.SendAsync("broadcastMessage", "system", $"{disconectedUserName} left the chatroom");
            return base.OnDisconnectedAsync(exception);

        }
    }
}