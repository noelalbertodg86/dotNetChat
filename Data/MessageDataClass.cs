using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Data
{
    /// <summary>
    /// Messages LINQ manager
    /// </summary>
    /// <returns></returns>
    public class MessageDataClass
    {
        private ChatDBEntities entities = null;
        public MessageDataClass()
        {
            entities = new ChatDBEntities();
            
        }

        /// <summary>
        /// Save message to data base
        /// </summary>
        /// <param name="message"> Object with all message properties</param>
        /// <returns></returns>
        public void SaveMessage(Message message)
        {
            entities.Messages.Add(message);
            entities.SaveChanges();
        }

        /// <summary>
        /// return the list of last 50 message in the chatroom 
        /// </summary>
        /// <param name="chatRoomId">chatroom id</param>
        /// <returns>Message List</returns>
        public List<Message> GetLast50ChatRoomMessages(int chatRoomId)
        {
            return entities.Messages.Where(p => p.ChatRoomID == chatRoomId).OrderByDescending(q => q.MessageDateTime).Take(50).ToList();
        }
    }
}
