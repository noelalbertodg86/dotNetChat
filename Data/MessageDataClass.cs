using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Data
{
    public class MessageDataClass
    {
        private ChatDBEntities entities = null;
        public MessageDataClass()
        {
            entities = new ChatDBEntities();
            
        }
        public void SaveMessage(Message message)
        {
            entities.Messages.Add(message);
            entities.SaveChanges();
        }

        public List<Message> GetLast50ChatRoomMessages(int chatRoomId)
        {
            return entities.Messages.Where(p => p.ChatRoomID == chatRoomId).OrderByDescending(q => q.MessageDateTime).Take(50).ToList();
        }
    }
}
