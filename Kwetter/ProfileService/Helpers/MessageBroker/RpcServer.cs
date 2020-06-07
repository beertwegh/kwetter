using System;
using System.Text;
using AuthService.Helpers;
using AuthService.Helpers.MessageBroker;
using ProfileService.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProfileService.Helpers.MessageBroker
{
    public class RpcServer : IRpcServer
    {
        private readonly IProfileService _profileService;
        private readonly IPersistentConnection _persistentConnection;

        public RpcServer(IPersistentConnection persistentConnection, IProfileService profileService)
        {
            _persistentConnection = persistentConnection;
            _profileService = profileService;
            GetUserName();

        }

        public void GetUserName()
        {
            var channel = _persistentConnection.Channel;
            channel.QueueDeclare(queue: "getusername", durable: false,
                exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "getusername",
                autoAck: false, consumer: consumer);
            Console.WriteLine(" [x] Awaiting RPC requests for username");

            consumer.Received += (model, ea) =>
            {
                string response = null;

                var body = ea.Body;
                var props = ea.BasicProperties;
                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                try
                {
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine(message);
                    var guid = Guid.Parse(message);
                    response = _profileService.GetUserName(guid);
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                        basicProperties: replyProps, body: responseBytes);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag,
                        multiple: false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(" [.] " + e.Message);
                    response = "";
                }
                finally
                {
                }
            };

        }
    }
}