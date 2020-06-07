using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using MessageService.Helpers.MessageBroker;

namespace MessageService.Helpers.MessageBroker
{
    public class SendMessageBroker : ISendMessageBroker
    {
        private const string MessageServiceExchange = "MessageService";

    }
}
