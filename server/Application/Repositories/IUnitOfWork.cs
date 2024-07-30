using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Persistence.Repositories
{
    /// <summary>
    /// Central unit to save modified entities
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Get a certain repository by entity type
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        IRepository<T> GetRepository<T>() where T : class, IEntity;

        /// <summary>
        /// Save all modified entities
        /// </summary>
        Task<int> Save(CancellationToken cancellationToken);

        /// <summary>
        /// Revered modified non applied changes
        /// </summary>
        Task Rollback();
    }
}