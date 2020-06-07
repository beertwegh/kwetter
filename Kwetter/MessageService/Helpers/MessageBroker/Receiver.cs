﻿using System;
using System.Text;
using Google.Protobuf;
using MessageService.Helpers.MessageBroker.Connection;
using MessageService.Services;
using Newtonsoft.Json;
using ProfileService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageService.Helpers.MessageBroker
{
    public class Receiver : IReceiver
    {

        private readonly IPersistentConnection _persistentConnection;
        private const string ProfileServiceExchange = "ProfileService";
        private readonly IMessageService _messageService;
        public Receiver(IPersistentConnection persistentConnection, IMessageService messageService)
        {
            _persistentConnection = persistentConnection;
            _messageService = messageService;
            ProfileService();
        }
        public void ProfileService()
        {
            var channel = _persistentConnection.Channel;
            channel.ExchangeDeclare(exchange: ProfileServiceExchange,
                type: "direct");
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                exchange: ProfileServiceExchange,
                routingKey: "EditProfileName");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                Console.WriteLine("Received sometginh");
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var routingKey = ea.RoutingKey;
                switch (routingKey)
                {
                    case "EditProfileName":
                        var user = JsonConvert.DeserializeObject<EditProfileName>(message);
                        _messageService.EditProfileName(user);
                        break;
                }
            };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);


        }

    }
}
