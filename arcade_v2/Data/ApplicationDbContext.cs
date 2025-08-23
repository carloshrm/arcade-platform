using arcade_v2.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace arcade_v2.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().Ignore(fields => fields.PhoneNumber);
            builder.Entity<ApplicationUser>().Ignore(fields => fields.PhoneNumberConfirmed);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Scores)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.Id)
                .HasPrincipalKey(e => e.Id);
        }
    }

}
