using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DbContext
{
    public class AuthContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }
        #region DbSets

        public DbSet<AuthUser> AuthUsers { get; set; }
        #endregion

    }
}
