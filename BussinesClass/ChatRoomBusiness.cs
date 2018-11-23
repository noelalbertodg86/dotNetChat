using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace BussinesClass
{
    public class ChatRoomBusiness
    {
        ChatRoomDataClass chatRoomManager = null;
        public ChatRoomBusiness()
        {
            chatRoomManager = new ChatRoomDataClass();
        }

        public List<ChatRoom> GetAllChatRoom()
        {
            return chatRoomManager.GetAllChatRooms();
        }

        public ChatRoom GetChatRoom(int chatRoomId)
        {
            return chatRoomManager.GetChatRoom(chatRoomId);
        }

        public bool CreateChatRoom(string name)
        {
            if (chatRoomManager.GetChatRoom(chatRoomName: name) == null)
            {
                ChatRoom newChatRoom = new ChatRoom();
                newChatRoom.Name = name;
                chatRoomManager.CreateChatRoom(newChatRoom);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
