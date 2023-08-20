using Domain;

namespace Persistence.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, IAuditableEntity;
        Task<int> Save(CancellationToken cancellationToken);
        Task Rollback();
    }
}