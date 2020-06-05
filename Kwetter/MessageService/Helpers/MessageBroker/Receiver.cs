using MessageService.Messaging;

namespace MessageService.Helpers.MessageBroker
{
    public class Receiver : IReceiver
    {
     
        private readonly IPersistentConnection _persistentConnection;
        public Receiver( IPersistentConnection persistentConnection)
        {
            _persistentConnection = persistentConnection;

        }
    }
}
