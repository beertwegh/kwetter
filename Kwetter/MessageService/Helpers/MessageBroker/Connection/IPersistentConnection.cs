using System;
using RabbitMQ.Client;

namespace MessageService.Helpers.MessageBroker.Connection
{
    public interface IPersistentConnection : IDisposable
    {
        public IModel Channel { get; }
    }
}
