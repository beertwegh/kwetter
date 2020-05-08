using RabbitMQ.Client;
using System;

namespace AuthService.Helpers.MessageBroker
{
    public interface IPersistentConnection : IDisposable
    {
        public IModel Channel { get; }
    }
}
