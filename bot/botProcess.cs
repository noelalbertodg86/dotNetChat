﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using Newtonsoft.Json;
using RabbitManager;
using CustomizeException;

namespace bot
{
    /// <summary>
    /// BotProcess class manage de process of get stock data from online services and return it to chatroom
    /// </summary>
    public class BotProcess
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["url"].ToString();
        private readonly string returnMessage = ConfigurationManager.AppSettings["returnMessage"].ToString();
        private static BotProcess botInstance = null;
        private RabbitManager.SendRabbitMQMessage rabbitManager = new SendRabbitMQMessage();

        /// <summary>
        /// The class is Singleton 
        /// </summary>
        public static BotProcess GetInstance()
        {
            if (botInstance == null)
                botInstance = new BotProcess();

            return botInstance;
        }

        //class constructor
        private BotProcess()
        {
        }

        /// <summary>
        /// getStock orchestrate de process to get stock, serialize and send de result
        /// </summary>
        /// <param name="IncomingMessage">json rabbit message</param>
        /// <returns>resultMessage</returns>
        public string getStock(string IncomingMessage)
        {
            RabbitMessage inputMessage = new RabbitMessage();
            try
            {
                //handlers are managed with dependency injection through classes interfaces, 
                //this allows in the future to add other handlers without much modification in the code

                string resultMessage = string.Empty;
                string ConfigParameters = string.Empty;

                inputMessage = JsonConvert.DeserializeObject<RabbitMessage>(IncomingMessage);
                try
                {
                    //reading the paramters in the configuration for the current command in this case "APPL"
                    ConfigParameters = ConfigurationManager.AppSettings[inputMessage.Message].ToString();
                }
                catch
                {
                    throw new BotNotImplementedCommandException($"Sorry the command [{inputMessage.Message}] is not implemented");
                }

                //invoke the handler 
                AdapterManager adapterManager = new AdapterManager();
                resultMessage = adapterManager.getInstanceByParam(inputMessage.Message, ConfigParameters).Execute();
                //return the result
                SendBotResponse(inputMessage.IdMessage, resultMessage);
                return resultMessage;
                
            }
            catch (BotNotImplementedCommandException e)
            {
                SendBotResponse(inputMessage.IdMessage, e.Message);
                return e.Message;
            }
            catch (Exception err)
            {
                string messageError = "getStock method error " + err.Message + " - " + err.StackTrace;
                EventLog.WriteEntry("BOT", messageError, EventLogEntryType.Error);
                Console.WriteLine(messageError);
                SendBotResponse(inputMessage.IdMessage, "Sorry an undefined error has occurred.");
                return messageError;
            }

        }
        
        /// <summary>
        /// in order to send back the result of a bot call, is create a new queue with a unique idMessage:bot this queue is listen by the
        /// request sender who was the first to set the idMessage value
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <param name="OutputMessage"></param>
        /// <returns></returns>
        public void SendBotResponse(string IdMessage, string OutputMessage)
        {
            string hostName = ConfigurationManager.AppSettings["localhost"].ToString();
            string[] queueList = { IdMessage, ConfigurationManager.AppSettings["queue"].ToString() };

            string botQueue = string.Join(":", queueList);
            rabbitManager.Send(OutputMessage, IdMessage, botQueue);

           Console.WriteLine(" [x] Queue [{0}] Sent {1}", botQueue, OutputMessage);
                
            
        }
    }
}
