using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;
using Newtonsoft.Json;

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
                RabbitMessage botResponse = new RabbitMessage();
                string IdMessage = Guid.NewGuid().ToString();
                Send(message, IdMessage,queueBot);
                int count = 0;
                while (string.IsNullOrEmpty(botResponse.Message) && count < 20)
                {
                    Thread.Sleep(1000);
                    botResponse = Recieve(IdMessage);
                    count++;
                }
                
                return botResponse.Message;
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
        /// <param name="idMessage">IdMensaje to mark the result</param>
        /// <returns></returns>
        public void Send(string message, string idMessage, string destinyQueue)
        {
            try
            {
                string result = string.Empty;
                RabbitMessage messageToSend = new RabbitMessage(idMessage, message);

                string jsonMessage = JsonConvert.SerializeObject(messageToSend);

                var factory = new ConnectionFactory() { HostName = hostName };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: destinyQueue,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(jsonMessage);

                    channel.BasicPublish(exchange: "",
                                            routingKey: destinyQueue,
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
        public RabbitMessage Recieve(string IdMessage)
        {
            RabbitMessage rabbitMessage = new RabbitMessage();
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
                            rabbitMessage = JsonConvert.DeserializeObject<RabbitMessage>(data);

                        }
                    }
                }
            }
            return rabbitMessage;
        
        }


    }

}