using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DbContext
{
    public class AuthContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AuthUser>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
        #region DbSets

        public DbSet<AuthUser> AuthUsers { get; set; }
        #endregion

    }
}
