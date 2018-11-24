﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using BussinesClass;
using Entities;
using RabbitManager;
using Encrypt;
using Newtonsoft.Json;
using CustomizeException;

namespace Server.Controllers
{

    public class ChatController : Controller
    {
        private IHubContext<ChatHub> _hubContext;
        private UserBusiness user;
        private MessagesBusiness messages;
        private SendRabbitMQMessage rabbitManager;
        private ChatHub chatHub;
        private Encrypt.EncryptManager encrypt;
        private UserSessionBusiness userSessionManager;
    


        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
            user = new UserBusiness();
            messages = new MessagesBusiness();
            rabbitManager = new SendRabbitMQMessage();
            chatHub = new ChatHub();
            encrypt = new EncryptManager();
            userSessionManager = new UserSessionBusiness();
        }


        /// <summary>
        /// in this funtion de user make login in the sistem using encrypt password, and load the las 50 messages 
        /// of the current chatroom, also al connected user is notify  
        /// </summary>
        /// <param name="userLoginData"></param>
        /// <returns>string json with result</returns>
        [HttpPost]
        [Route("api/chat/login")]
        public string Login([FromBody]UserCredential userLoginData)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                //the password is encrypted for  security
                userLoginData.password = encrypt.EncryptTextBase64(userLoginData.password);
                //validate login credentials
                Entities.User loginUser = user.Login(userLoginData.user, userLoginData.password);
                //save the user section
                userSessionManager.SetLoginUserSession(loginUser.UserID, chatHub.getConnectionId());

                //get the 50 or less last save message in the current chatroom
                List<Message> last50Messages = messages.GetLast50ChatRoomMessages(userLoginData.chatRoomId);
                foreach (Message m in last50Messages)
                {
                    //broadcast the  obtained messages to the new conected user
                    _hubContext.Clients.Client(chatHub.getConnectionId()).SendAsync("broadcastMessage", m.OriginUserID, m.MessageText);

                }
                // notify all connected users of the new user except the user who logged in
                _hubContext.Clients.AllExcept(chatHub.getConnectionId()).SendAsync("recieveMessage", $"Welcome to chat {userLoginData.user}");

                //build and return the ok response
                responseMessage.Codigo = "OK";
                responseMessage.MensajeRetorno = "Login successfully";
                return JsonConvert.SerializeObject(responseMessage);
            }
            catch (UserOrPasswordIncorrectException e)
            {
                string error = e.Message;
                responseMessage.Codigo = "Error";
                responseMessage.MensajeRetorno = error;
                return JsonConvert.SerializeObject(responseMessage);
            }
            catch (Exception e)
            {
                string error = e.Message;
                responseMessage.Codigo = "Error";
                responseMessage.MensajeRetorno = error;
                return JsonConvert.SerializeObject(responseMessage);
            }

            
        }

        /// <summary>
        ///  this function manage the user messages and invoke the bot for get stock if it is called
        /// </summary>
        /// <param name="userLoginData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/chat/message")]
        public void Messagemanager([FromBody]ChatMessaje chatMessajeInput)
        {
            try
            {
                string resultMessage = chatMessajeInput.message;
                System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                //if some user write de comand for invoke the bot
                if (resultMessage.Trim().Replace("/", "") == "/stock=APPL​".Trim().Replace("/",""))
                {
                    //it is set a call in rabbitMQ in order of bot get and return the stock
                    resultMessage = rabbitManager.SendAndRecieve(chatMessajeInput.message);
                    chatMessajeInput.user = "BOT";
                }
                else
                {
                    //the send message is save into data base
                    messages.SaveUserToChatRoomMessage(chatMessajeInput.user, chatMessajeInput.chatroom, chatMessajeInput.message);
                }
                //broad cast the message to all conected users
                _hubContext.Clients.All.SendAsync("broadcastMessage", chatMessajeInput.user, resultMessage);
            }
            catch (Exception e)
            {
                string error = e.Message;
                
            }


        }
        /// <summary>
        ///  this function create a new user in the system
        /// </summary>
        /// <param name="userLoginData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/chat/createuser")]
        public string CreateUser([FromBody]Entities.User newUser)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                newUser.Password = encrypt.EncryptTextBase64(newUser.Password);
                bool newUserResult = user.CreateNewUser(newUser.Name, newUser.LastName, newUser.Birthday, newUser.Email,
                                                        newUser.UserName, newUser.Password, newUser.CompanyDepartment);

                responseMessage.Codigo = "OK";
                responseMessage.MensajeRetorno = "User created successfully";
                return JsonConvert.SerializeObject(responseMessage);

            }
            catch (UserTakenException e)
            {
                string error = e.Message;
                responseMessage.Codigo = "Error";
                responseMessage.MensajeRetorno = error;
                return JsonConvert.SerializeObject(responseMessage);
            }
            catch (Exception e)
            {
                string error = e.Message;
                responseMessage.Codigo = "Error";
                responseMessage.MensajeRetorno = error;
                return JsonConvert.SerializeObject(responseMessage);
            }


        }
    }

}