using System;
using System.Text;
using AuthService.Helpers.MessageBroker;
using AuthService.Messaging;
using Newtonsoft.Json;
using ProfileService.Models;
using ProfileService.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProfileService.Helpers.MessageBroker
{
    public class Receiver : IReceiver
    {
        private readonly IProfileService _profileService;
        private readonly IPersistentConnection _persistentConnection;
        public Receiver(IPersistentConnection persistentConnection, IProfileService profileService)
        {
            _persistentConnection = persistentConnection;
            _profileService = profileService;
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
                          var user = JsonConvert.DeserializeObject<UserRegistrationModel>(message);
                          _profileService.UserRegistered(user);
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
                          _profileService.UserDeleted(Guid.Parse(message));
                          break;
                  }
              };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);


        }


    }
}
