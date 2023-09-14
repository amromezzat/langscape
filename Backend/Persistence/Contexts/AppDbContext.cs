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
        public DbSet<FlashCardSetFavorite> FlashCardSetFavorites { get; set; }

        public AppDbContext(DbContextOptions options, IAuditableEntitiesInterceptor auditableEntitiesInterceptor) : base(options)
        {
            _auditableEntitiesInterceptor = auditableEntitiesInterceptor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            AddAuditableEntityIndices(builder);

            builder.Entity<FlashCardsWord>()
                .HasOne(word => word.Set)
                .WithMany(set => set.Words)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FlashCardSetFavorite>(favorite => 
            {
                favorite.HasKey(key => new {key.AppUserId, key.SetId});

                favorite.HasOne(favorite => favorite.AppUser)
                    .WithMany()
                    .HasForeignKey(favorite => favorite.AppUserId)
                    .OnDelete(DeleteBehavior.Cascade);

                favorite.HasOne(favorite => favorite.Set)
                    .WithMany()
                    .HasForeignKey(favorite => favorite.SetId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.AddInterceptors(_auditableEntitiesInterceptor);
        }

        private static void AddAuditableEntityIndices(ModelBuilder builder)
        {
            foreach (var derivedType in typeof(IAuditableEntity).GetImplementingClasses())
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
    }
}