using MessageService.DbContext;
using MessageService.Models;
using MessageService.Repository.Interface;
using ProfileService.Models;
using System.Collections.Generic;
using System.Linq;

namespace MessageService.Repository
{
    public class MessageRepository : IMessageRepository

    {
        private readonly MessageDbContext _messageDbContext;

        public MessageRepository(MessageDbContext messageDbContext)
        {
            _messageDbContext = messageDbContext;
        }

        public List<Message> GetAllMessages()
        {
            return this._messageDbContext.Messages.ToList();
        }

        public void EditProfileName(EditProfileName model)
        {
            var list = _messageDbContext.Messages.Where(m => m.UserId == model.UserId).ToList();
            list.ForEach(s => s.UserName = model.NewName);
            _messageDbContext.SaveChanges();
        }

        public void SaveNewMessage(Message message)
        {
            _messageDbContext.Messages.Add(message);
            _messageDbContext.SaveChanges();
        }
    }
}
