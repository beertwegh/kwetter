using System.Collections.Generic;
using MessageService.Models;

namespace MessageService.Repository.Interface
{
    public interface IMessageRepository
    {
        void SaveNewMessage(Message message);
        List<Message> GetAllMessages();
    }
}
