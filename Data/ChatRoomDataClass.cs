using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Data
{
    /// <summary>
    /// ChatRoom LINQ manager
    /// </summary>
    /// <returns></returns>
    public class ChatRoomDataClass
    {
        private ChatDBEntities entities = null;
        public ChatRoomDataClass()
        {
            entities = new ChatDBEntities();

        }

        /// <summary>
        /// Create chatroom
        /// </summary>
        /// <param name="chatRoom"> Object with all chat room properties</param>
        /// <returns></returns>
        public void CreateChatRoom(ChatRoom chatRoom)
        {
            entities.ChatRooms.Add(chatRoom);
            entities.SaveChanges();
        }

        /// <summary>
        /// get all chatroom en data base
        /// </summary>
        /// <param name="chatRoom"></param>
        /// <returns>List of chat room</returns>
        public List<ChatRoom> GetAllChatRooms()
        {
            return entities.ChatRooms.ToList();
        }

        /// <summary>
        /// get  chatroom for id
        /// </summary>
        /// <param name="chatRoomId">chatroom id</param>
        /// <returns>one ChatRoom class or null</returns>
        public ChatRoom GetChatRoom(int chatRoomId)
        {
            return entities.ChatRooms.Where(p => p.ChatRoomID == chatRoomId).FirstOrDefault();
        }


        /// <summary>
        /// get  chatroom for name
        /// </summary>
        /// <param name="chatRoomName">chatroom name</param>
        /// <returns>one ChatRoom class or null</returns>
        public ChatRoom GetChatRoom(string chatRoomName)
        {
            return entities.ChatRooms.Where(p => p.Name == chatRoomName).FirstOrDefault();
        }
    }
}
