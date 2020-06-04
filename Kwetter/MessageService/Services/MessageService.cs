using MessageService.Helpers.MessageBroker;
using MessageService.Repository.Interface;

namespace MessageService.Services
{
    public class MessageService : IMessageService
    {
        private readonly ISendMessageBroker _messageBroker;
        private readonly IMessageRepository _messageRepository;
        public MessageService(ISendMessageBroker messageBroker, IMessageRepository messageRepository)
        {
            _messageBroker = messageBroker;
            _messageRepository = messageRepository;
        }

   
    }
}
