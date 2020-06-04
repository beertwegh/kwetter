using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageService.Models;
using RabbitMQ.Client;

namespace MessageService.Helpers.MessageBroker
{
    public class SendMessageBroker : ISendMessageBroker
    {
        private const string UserServiceExchange = "UserService";

        public void Registration(UserRegistrationModel user)
        {
            Task.Run(() =>
            {

                var factory = new ConnectionFactory() {HostName = "localhost"};
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: UserServiceExchange,
                        type: "direct");

                    var body = Encoding.UTF8.GetBytes(user.ToJson());

                    channel.BasicPublish(exchange: UserServiceExchange,
                        routingKey: "Register",
                        basicProperties: null,
                        body: body);
                }

            });
        }
    }
}
