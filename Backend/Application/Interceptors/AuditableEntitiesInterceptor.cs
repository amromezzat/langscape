using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Application.Interceptors
{
    public class AuditableEntitiesInterceptor : SaveChangesInterceptor
    {
        private readonly IUserAccessor _userAccessor;

        public AuditableEntitiesInterceptor(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null)
            {
                return base.SavingChanges(eventData, result);
            }

            AddTimestamps(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            AddTimestamps(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void AddTimestamps(DbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries<IAuditableEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = _userAccessor.GetUserId();
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedBy = _userAccessor.GetUserId();
                    entry.Entity.ModifiedAt = DateTime.UtcNow;
                }
            }
        }
    }
}