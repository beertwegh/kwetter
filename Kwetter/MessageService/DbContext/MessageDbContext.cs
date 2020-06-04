using Microsoft.EntityFrameworkCore;

namespace MessageService.DbContext
{
    public class MessageDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
        #region DbSets

        
        #endregion

    }
}



