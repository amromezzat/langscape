using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Interceptors;

namespace Persistence.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        private readonly IAuditableEntitiesInterceptor _auditableEntitiesInterceptor;

        public DbSet<FlashCardsWord> FlashCardWords { get; set; }
        public DbSet<FlashCardsSet> FlashCardSets { get; set; }

        public AppDbContext(DbContextOptions options, IAuditableEntitiesInterceptor auditableEntitiesInterceptor) : base(options)
        {
            _auditableEntitiesInterceptor = auditableEntitiesInterceptor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FlashCardsWord>()
                .HasOne(a => a.Set)
                .WithMany(c => c.Words)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.AddInterceptors(_auditableEntitiesInterceptor);
        }
    }
}