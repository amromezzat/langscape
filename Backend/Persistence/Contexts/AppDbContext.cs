using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<FlashCardWord> FlashCardWords { get; set; }
        public DbSet<FlashCardSet> FlashCardSets { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FlashCardWord>()
                .HasOne(a => a.Set)
                .WithMany(c => c.Words)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}