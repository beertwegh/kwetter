using System;
using System.Collections.Generic;
using MessageService.Models;
using ProfileService.Models;

namespace MessageService.Repository.Interface
{
    public interface IMessageRepository
    {
        void SaveNewMessage(Message message);
        List<Message> GetAllMessages();
        void EditProfileName(EditProfileName model);
        void UserDeleted(Guid userId);
    }
}
