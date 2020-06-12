using RabbitMQ.Client;

namespace AuthService.Helpers.MessageBroker.Connection
{
    public class PersistentConnection : IPersistentConnection
    {
        public IConnection Connection { get; }
        public IModel Channel { get; }

        public PersistentConnection()
        {
            var factory = new ConnectionFactory() { HostName = "10.12.3.98" };
            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();
        }

        public void Dispose()
        {
            Connection?.Dispose();
            Channel?.Dispose();
        }
    }
}
