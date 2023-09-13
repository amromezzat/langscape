using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Interceptors;
using Shared.Extensions;

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

            foreach(var derivedType in typeof(IAuditableEntity).GetImplementingClasses()) 
            {
                builder.Entity(derivedType)
                    .HasOne(nameof(IAuditableEntity.CreatedBy))
                    .WithMany()
                    .HasForeignKey(nameof(IAuditableEntity.CreatedById))
                    .OnDelete(DeleteBehavior.SetNull);

                builder.Entity(derivedType)
                    .HasOne(nameof(IAuditableEntity.ModifiedBy))
                    .WithMany()
                    .HasForeignKey(nameof(IAuditableEntity.ModifiedById))
                    .OnDelete(DeleteBehavior.SetNull);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.AddInterceptors(_auditableEntitiesInterceptor);
        }
    }
}