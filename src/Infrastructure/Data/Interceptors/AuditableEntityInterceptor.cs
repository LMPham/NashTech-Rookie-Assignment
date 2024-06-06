using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Domain.Common;

namespace Infrastructure.Data.Interceptors
{
    /// <summary>
    /// Interceptor for updating the creators and modifiers of
    /// auditable entities.
    /// </summary>
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly IUser user;
        private readonly TimeProvider timeProvider;

        public AuditableEntityInterceptor(
            IUser _user,
            TimeProvider _timeProvider)
        {
            user = _user;
            timeProvider = _timeProvider;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Update creator and modifier of created and modified entities.
        /// </summary>
        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity<int>>())
            {
                if (entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    var utcNow = timeProvider.GetUtcNow();
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.CreatedBy = user.Id;
                        entry.Entity.CreatedByUserName = user.UserName;
                        entry.Entity.Created = utcNow;
                    }
                    entry.Entity.LastModifiedBy = user.Id;
                    entry.Entity.LastModifiedByUserName = user.UserName;
                    entry.Entity.LastModified = utcNow;
                }
            }
        }
    }

    public static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
