using AuthService.Models;
using AuthService.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace AuthService.Helpers.MessageBroker
{
    public class Receiver : IReceiver
    {
        private readonly IAuthService _authService;
        private readonly IPersistentConnection _persistentConnection;
        public Receiver(IAuthService authService, IPersistentConnection persistentConnection)
        {
            _authService = authService;
            _persistentConnection = persistentConnection;
            Registration();
            DeleteUser();
        }

        private const string UserServiceExchange = "UserService";

        public void Registration()
        {
            var channel = _persistentConnection.Channel;
            channel.ExchangeDeclare(exchange: UserServiceExchange,
                    type: "direct");
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                    exchange: UserServiceExchange,
                    routingKey: "Register");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
              {
                  var body = ea.Body;
                  var message = Encoding.UTF8.GetString(body.ToArray());
                  var routingKey = ea.RoutingKey;
                  switch (routingKey)
                  {
                      case "Register":
                          var user = JsonConvert.DeserializeObject<AuthUser>(message);
                          _authService.UserRegistered(user);
                          break;
                  }
              };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }
        public void DeleteUser()
        {
            var channel = _persistentConnection.Channel;
            channel.ExchangeDeclare(exchange: UserServiceExchange,
                type: "direct");
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                exchange: UserServiceExchange,
                routingKey: "DeleteUser");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var routingKey = ea.RoutingKey;
                switch (routingKey)
                {
                    case "DeleteUser":
                        _authService.UserDeleted(UserId:Guid.Parse(message));
                        break;
                }
            };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);


        }


    }
}
