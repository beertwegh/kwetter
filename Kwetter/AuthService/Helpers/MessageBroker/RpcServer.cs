using System;
using System.Text;
using AuthService.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AuthService.Helpers.MessageBroker
{
    public class RpcServer : IRpcServer
    {
        private readonly IAuthService _authService;
        private readonly IPersistentConnection _persistentConnection;

        public RpcServer(IAuthService authService, IPersistentConnection persistentConnection)
        {
            _authService = authService;
            _persistentConnection = persistentConnection;
            GetUserId();

        }

        public void GetUserId()
        {
            var channel = _persistentConnection.Channel;
            channel.QueueDeclare(queue: "getuserid", durable: false,
                exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "getuserid",
                autoAck: false, consumer: consumer);
            Console.WriteLine(" [x] Awaiting RPC requests");

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
                    response = _authService.GetClaim(message);
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