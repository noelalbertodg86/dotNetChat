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

        public List<MessageView> GetLast50ChatRoomMessages(int chatRoomId)
        {
            return messageManager.GetLast50ChatRoomMessages(chatRoomId);
        }

        public bool SaveUserToChatRoomMessage(int userId, int chatRoomId, string text)
        {
            try
            {
                Messages newMessage = new Messages();
                newMessage.MessageText = text;
                newMessage.OriginUserId = userId;
                newMessage.ChatRoomId = chatRoomId;
                newMessage.DestinyUserId = null;
                newMessage.MessageDateTime = DateTime.Now;

                messageManager.SaveMessage(newMessage);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }


        public bool SaveUserToChatRoomMessage(string userName, int chatRoomId, string text)
        {
            try
            {
                User user_ = userManager.GetUser(userName);
                if (user_ != null)
                {
                    Messages newMessage = new Messages();
                    newMessage.MessageText = text;
                    newMessage.OriginUserId = user_.UserId;
                    newMessage.ChatRoomId = chatRoomId;
                    newMessage.DestinyUserId = null;
                    newMessage.MessageDateTime = DateTime.Now;

                    messageManager.SaveMessage(newMessage);
                    return true;
                }
                else
                {

                    return false;

                }

            }
            catch (Exception e)
            {
                return false;
            }
        }


        public void SaveUserToUserMessage(int originUserId, int destinyUserId, string text)
        {
            Messages newMessage = new Messages();
            newMessage.MessageText = text;
            newMessage.OriginUserId = originUserId;
            newMessage.ChatRoomId = null;
            newMessage.DestinyUserId = destinyUserId;
            newMessage.MessageDateTime = DateTime.Now;

            messageManager.SaveMessage(newMessage);
        }
    }
}
