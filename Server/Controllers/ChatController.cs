using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using BussinesClass;
using RabbitManager;
using Encrypt;
using Newtonsoft.Json;
using CustomizeException;
using Entities;

namespace Server.Controllers
{

    public class ChatController : Controller
    {
        private IHubContext<ChatHub> _hubContext;
        private UserBusiness user;
        private MessagesBusiness messages;
        private SendRabbitMQMessage rabbitManager;
        private EncryptManager encrypt;
        private UserSessionBusiness userSessionManager;




        public ChatController(IHubContext<ChatHub> hubContext)
        {
                _hubContext = hubContext;
                user = new UserBusiness();
                messages = new MessagesBusiness();
                rabbitManager = new SendRabbitMQMessage();
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
                Entities.User loginUser =user.Login(userLoginData.user, userLoginData.password);
                //save the user section
                userSessionManager.SetLoginUserSession(loginUser.UserId, userLoginData.conectionId, userLoginData.user);

                //get the 50 or less last save message in the current chatroom
                List<MessageView> last50Messages = messages.GetLast50ChatRoomMessages(userLoginData.chatRoomId);
                foreach (MessageView m in last50Messages)
                {
                    //broadcast the  obtained messages to the new conected user
                     _hubContext.Clients.Client(userLoginData.conectionId).SendAsync("broadcastMessage", m.OriginUserName, m.MessageText);

                }
                // notify all connected users of the new user except the user who logged in
                 _hubContext.Clients.AllExcept(userLoginData.conectionId).SendAsync("recieveMessage", $"Welcome to chat {loginUser.Name}");

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
        public string MessageManager([FromBody]ChatMessaje chatMessajeInput)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                string resultMessage = chatMessajeInput.message;
                //if some user write de comand for invoke the bot, 
                //for improve the command management if the user type /stock=APPL the system handler
                //any other command the can come in the BOT, that is way the question StartsWith("/stock=")
                if (resultMessage.Trim().StartsWith("/stock="))
                {
                    //it is set a call in rabbitMQ in order of bot get and return the stock
                    resultMessage = rabbitManager.SendAndRecieve(resultMessage.Split("=")[1]);
                    chatMessajeInput.user = "BOT";
                }
                else
                {
                    //the send message is save into data base
                    messages.SaveUserToChatRoomMessage(chatMessajeInput.user, chatMessajeInput.chatroom, chatMessajeInput.message);
                     
                }
                //broad cast the message to all conected users
                 _hubContext.Clients.All.SendAsync("broadcastMessage", chatMessajeInput.user, resultMessage);

                responseMessage.Codigo = "OK";
                responseMessage.MensajeRetorno = "Message send successfully";
                return JsonConvert.SerializeObject(responseMessage);
            }
            catch (Exception e)
            {
                string error = e.Message;
                responseMessage.Codigo = "Error";
                responseMessage.MensajeRetorno = "An error was occur sending message";
                return JsonConvert.SerializeObject(responseMessage);
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