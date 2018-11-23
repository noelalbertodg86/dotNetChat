using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Configuration;
using System.Threading;
using System.IO;

namespace RabbitManager
{
    /// <summary>
    /// class to manage set and get message throw queue in rabbitMQ
    /// </summary>
    /// <returns></returns>
    public class SendRabbitMQMessage
    {
        string hostName = "localhost";
        string queueBot = "bot";
        string botResult = string.Empty;

        public SendRabbitMQMessage()
        {
        }

        public SendRabbitMQMessage(string hostNameInput, string queue)
        {
            hostName = hostNameInput;
            queueBot = queue;
        }

        /// <summary>
        ///  set a  message throw queue in rabbitMQ and get the response 
        /// </summary>
        /// <param name="message">message to send by the default queue</param>
        /// <returns></returns>
        public string SendAndRecieve(string message)
        {
            try
            {
                string botResponse = string.Empty;
                string IdMessage = Guid.NewGuid().ToString();
                Send(message, IdMessage);
                int count = 0;
                while (string.IsNullOrEmpty(botResponse) && count < 10)
                {
                    botResponse = Recieve(IdMessage);
                    Thread.Sleep(400);
                    count++;
                }
                
                return botResponse;
            }
            catch (Exception e)
            {
                string messageError = e.Message;
                return messageError;
            }

        }
        /// <summary>
        ///  set a  message throw queue in rabbitMQ
        /// </summary>
        /// <param name="message">message to send</param>
        /// <param name="IdMessage">IdMensaje to mark the result</param>
        /// <returns></returns>
        public void Send(string message, string IdMessage="")
        {
            try
            {
                if(IdMessage == "")
                {
                    IdMessage = Guid.NewGuid().ToString();
                }
                
                string result = string.Empty;
                string jsonMessage = "{\"IdMessage\":\"" + IdMessage + "\",\"Message\": \""+message+"\"}";

                var factory = new ConnectionFactory() { HostName = hostName };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueBot,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(jsonMessage);

                    channel.BasicPublish(exchange: "",
                                            routingKey: queueBot,
                                            basicProperties: null,
                                            body: body);



                }
            }
            catch (Exception e)
            {
                string messageError = e.Message;
            }

        }
        /// <summary>
        ///  get a  message from queue in rabbitMQ with GUID:queue
        /// </summary>
        /// <param name="IdMessage">to build the response queue IdMensaje:bot</param>
        /// <returns></returns>
        public string Recieve(string IdMessage)
        {
            string botQueue = IdMessage + ":" + queueBot;
            string data = string.Empty;
            {
                using (IConnection connection = new ConnectionFactory().CreateConnection(hostName))
                {
                    using (IModel channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(botQueue, false, false, false, null);
                        var consumer = new EventingBasicConsumer(channel);
                        BasicGetResult result = channel.BasicGet(botQueue, true);
                        if (result != null)
                        {
                            data = Encoding.UTF8.GetString(result.Body);
          
                        }
                    }
                }
            }
            return data;
        
        }


    }

}