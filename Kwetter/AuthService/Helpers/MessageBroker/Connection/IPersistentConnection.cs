using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace AuthService.Helpers.MessageBroker
{
    public interface IPersistentConnection : IDisposable
    {
        public IModel Channel { get; }
    }
}
