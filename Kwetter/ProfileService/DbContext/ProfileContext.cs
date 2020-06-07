using Microsoft.EntityFrameworkCore;
using ProfileService.Models;

namespace ProfileService.DbContext
{
    public class ProfileContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
        #region DbSets

        public DbSet<Profile> Profiles { get; set; }
        #endregion

    }
}
