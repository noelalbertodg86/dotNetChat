using System;
using System.Collections.Generic;
using Data;
using Entities;

namespace BussinesClass
{
    /// <summary>
    /// Class to manage the Messages entity 
    /// </summary>
    /// <returns></returns>
    public class MessagesBusiness
    {
        private MessageDataClass messageManager = null;
        private UserDataClass userManager = null;

        public MessagesBusiness()
        {
            messageManager = new MessageDataClass();
            userManager = new UserDataClass();
            
        }

        public List<Message> GetLast50ChatRoomMessages(int chatRoomId)
        {
            return messageManager.GetLast50ChatRoomMessages(chatRoomId);
        }

        public bool SaveUserToChatRoomMessage(int userId, int chatRoomId, string text)
        {

            Message newMessage = new Message();
            newMessage.MessageText = text;
            newMessage.OriginUserID = userId;
            newMessage.ChatRoomID = chatRoomId;
            newMessage.DestinyUserID = null;
            newMessage.MessageDateTime = DateTime.Now;

            messageManager.SaveMessage(newMessage);
            return true;

        }


        public bool SaveUserToChatRoomMessage(string userName, int chatRoomId, string text)
        {

                User user_ = userManager.GetUser(userName);
                if (user_ != null)
                {
                    Message newMessage = new Message();
                    newMessage.MessageText = text;
                    newMessage.OriginUserID = user_.UserID;
                    newMessage.ChatRoomID = chatRoomId;
                    newMessage.DestinyUserID = null;
                    newMessage.MessageDateTime = DateTime.Now;

                    messageManager.SaveMessage(newMessage);
                    return true;
                }
                else
                {

                    return false;

                }

        }


        public void SaveUserToUserMessage(int originUserId, int destinyUserId, string text)
        {
            Message newMessage = new Message();
            newMessage.MessageText = text;
            newMessage.OriginUserID = originUserId;
            newMessage.ChatRoomID = null;
            newMessage.DestinyUserID = destinyUserId;
            newMessage.MessageDateTime = DateTime.Now;

            messageManager.SaveMessage(newMessage);
        }
    }
}
