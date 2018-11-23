using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Configuration;

namespace bot
{
    class Program
    {
        /// <summary>
        /// Start listening the [bot queue] for incoming messages to start de bot process.
        /// The process is always listen no need for while(True) 
        /// </summary>
        /// <returns></returns>
        static void Main(string[] args)
        {
            string localHost = ConfigurationManager.AppSettings["localhost"].ToString();
            string botQueue = ConfigurationManager.AppSettings["queue"].ToString();

            var factory = new ConnectionFactory() { HostName = localHost };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: botQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    string stock = BotProcess.GetInstance().getStock(message);
                    Console.WriteLine(stock);
                    Console.WriteLine(" ...bot is listening [[{0}]] queue", botQueue);

                };
                channel.BasicConsume(queue: botQueue, autoAck: true, consumer: consumer);

                Console.WriteLine(" ...bot is listening [{0}] queue", botQueue);
                Console.ReadLine();
            }
        }
    }
}
