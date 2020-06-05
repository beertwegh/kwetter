using System;
using System.Collections.Generic;
using System.Linq;
using MessageService.DbContext;
using MessageService.Models;
using MessageService.Repository.Interface;

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
        public void SaveNewMessage(Message message)
        {
            _messageDbContext.Messages.Add(message);
            _messageDbContext.SaveChanges();
        }
    }
}
