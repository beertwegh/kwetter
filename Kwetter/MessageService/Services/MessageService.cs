using System;
using System.Collections.Generic;
using MessageService.Helpers.MessageBroker;
using MessageService.Models;
using MessageService.Repository.Interface;
using ProfileService.Models;

namespace MessageService.Services
{
    public class MessageService : IMessageService
    {
        private readonly ISendMessageBroker _messageBroker;
        private readonly IMessageRepository _messageRepository;
        private readonly IRpcCalls _rpcCalls;
        public MessageService(ISendMessageBroker messageBroker, IMessageRepository messageRepository, IRpcCalls rpcCalls)
        {
            _messageBroker = messageBroker;
            _messageRepository = messageRepository;
            _rpcCalls = rpcCalls;
        }

        public List<Message> GetAllMessages(Guid? userId = null)
        {
            if (userId.HasValue)
            {
                return null;
            }
            else
            {
                return _messageRepository.GetAllMessages();
            }
        }

        public void NewMessage(Message message, Guid userId)
        {
            message.UserId = userId;
            message.UserName = _rpcCalls.GetUserName(userId);
            _messageRepository.SaveNewMessage(message);
        }

        public void EditProfileName(EditProfileName model)
        {
            _messageRepository.EditProfileName(model);
        }

        public void UserDeleted(Guid userId)
        {
            _messageRepository.UserDeleted(userId);
        }
    }
}
