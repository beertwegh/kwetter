using System;
using ProfileService.Models;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Helpers.MessageBroker
{
    public class SendMessageBroker : ISendMessageBroker
    {
        private const string ProfileServiceExchange = "ProfileService";

        public void EditProfileName(EditProfileName model)
        {
            Task.Run(() =>
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: ProfileServiceExchange,
                        type: "direct");

                    var body = Encoding.UTF8.GetBytes(model.ToJson());
                    Console.WriteLine("sedning");
                    channel.BasicPublish(exchange: ProfileServiceExchange,
                        routingKey: "EditProfileName",
                        basicProperties: null,
                        body: body);
                }
            });
        }
    }
}
