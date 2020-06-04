using MessageService.DbContext;
using MessageService.Repository.Interface;

namespace MessageService.Repository
{
    public class MessageRepository : IMessageRepository

    {
        private readonly MessageDbContext _userDbContext;

        public MessageRepository(MessageDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

    }
}
