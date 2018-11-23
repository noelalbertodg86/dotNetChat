using Microsoft.AspNetCore.SignalR;

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
        public string getConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}