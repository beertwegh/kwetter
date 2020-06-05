using System;
using System.Collections.Generic;
using MessageService.Models;

namespace MessageService.Services
{
    public interface IMessageService
    {
        void NewMessage(Message message, Guid userId);
        List<Message> GetAllMessages(Guid? userId = null);
    }
}
