using RabbitMQ.Client;
using System;

namespace MessageService.Helpers.MessageBroker
{
    public interface IPersistentConnection : IDisposable
    {
        public IModel Channel { get; }
    }
}
