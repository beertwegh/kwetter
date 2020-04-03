using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DbContext
{
    public class AuthContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthUser>().HasData(
                new AuthUser
                {
                    UserId = 1,
                    Username = "bas",
                    Password = "test123"
                }
            );
        }
        #region DbSets

        public DbSet<AuthUser> AuthUsers { get; set; }
        #endregion

    }
}
