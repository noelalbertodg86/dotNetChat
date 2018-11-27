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
        private ChatDBContext entities = null;
        public MessageDataClass()
        {
            entities = new ChatDBContext();
            
        }

        /// <summary>
        /// Save message to data base
        /// </summary>
        /// <param name="message"> Object with all message properties</param>
        /// <returns></returns>
        public void SaveMessage(Messages message)
        {
            entities.Messages.Add(message);
            entities.SaveChanges();
        }

        /// <summary>
        /// return the list of last 50 message in the chatroom 
        /// </summary>
        /// <param name="chatRoomId">chatroom id</param>
        /// <returns>Message List</returns>
        public List<MessageView> GetLast50ChatRoomMessages(int chatRoomId, int topMessage=50)
        {
            using (var ctx = new ChatDBContext())
            {
                var query = from m in ctx.Messages
                            join u in ctx.User on m.OriginUserId equals u.UserId 
                            select new MessageView
                            {
                                MessageText = m.MessageText,
                                MessageDateTime = m.MessageDateTime,
                                OriginUserName = u.Name,
                                
                            };
                return query.ToList();
            }
            //return entities.Messages.Where(p => p.ChatRoomId == chatRoomId).OrderByDescending(q => q.MessageDateTime).Take(topMessage).ToList();
        }
    }
}
