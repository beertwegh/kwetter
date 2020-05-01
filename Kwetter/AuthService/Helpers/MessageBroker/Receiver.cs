using AuthService.Messaging;
using AuthService.Models;
using AuthService.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Helpers.MessageBroker
{
    public class Receiver : IReceiver
    {
        private readonly IAuthService _authService;

        public Receiver(IAuthService authService)
        {
            _authService = authService;
            Task.Run(async () =>
            {
                await UserService();
            });
        }

        private const string UserServiceExchange = "UserService";

        public async Task UserService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: UserServiceExchange,
                    type: "direct");
                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                        exchange: UserServiceExchange,
                        routingKey: "Register");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received +=async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    var routingKey = ea.RoutingKey;
                    switch (routingKey)
                    {
                        case "Register":
                            var user = JsonConvert.DeserializeObject<AuthUser>(message);
                            await _authService.UserRegistered(user);
                            break;
                    }
                };
                channel.BasicConsume(queue: queueName,
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
