using RabbitMQ.Client;
using System;
using System.Text;
// http://www.rabbitmq.com/getstarted.html
// http://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html

namespace Bnaya.Samples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                ConsoleKey k = ConsoleKey.Escape;
                do
                {
                    channel.QueueDeclare(queue: "hello",
                                                    durable: false,
                                                    exclusive: false,
                                                    autoDelete: false,
                                                    arguments: null);

                    string message = $"Hello World! {DateTimeOffset.UtcNow:yyyy-MM-dd HH:mm:ss}";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                    Console.WriteLine("Press [Esc] to exit any other key for sending");
                    k =   Console.ReadKey(true).Key;
                } while (k != ConsoleKey.Escape);
            }

        }
    }
}
