using System;
using System.Collections.Generic;
using MessageService.Models;
using ProfileService.Models;

namespace MessageService.Services
{
    public interface IMessageService
    {
        void NewMessage(Message message, Guid userId);
        List<Message> GetAllMessages(Guid? userId = null);
        void EditProfileName(EditProfileName model);
        void UserDeleted(Guid parse);
    }
}
