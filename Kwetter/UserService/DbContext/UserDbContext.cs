using MessageService.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageService.DbContext
{
    public class UserDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
        #region DbSets

        public DbSet<User> Users { get; set; }
        #endregion

    }
}



