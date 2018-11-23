using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Data
{
    public class ChatRoomDataClass
    {
        private ChatDBEntities entities = null;
        public ChatRoomDataClass()
        {
            entities = new ChatDBEntities();

        }

        public void CreateChatRoom(ChatRoom chatRoom)
        {
            entities.ChatRooms.Add(chatRoom);
            entities.SaveChanges();
        }

        public List<ChatRoom> GetAllChatRooms()
        {
            return entities.ChatRooms.ToList();
        }

        public ChatRoom GetChatRoom(int chatRoomId)
        {
            return entities.ChatRooms.Where(p => p.ChatRoomID == chatRoomId).FirstOrDefault();
        }

        public ChatRoom GetChatRoom(string chatRoomName)
        {
            return entities.ChatRooms.Where(p => p.Name == chatRoomName).FirstOrDefault();
        }
    }
}
