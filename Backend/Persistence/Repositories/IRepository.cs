using Domain;

namespace Persistence.Repositories
{
    /// <summary>
    /// Common 
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepository<T> where T: IEntity
    {
        /// <summary>
        /// Entities query
        /// </summary>
        IQueryable<T> Entities { get; }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Entity id</param>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Get all entities of type <typeparamref name="T"/>
        /// </summary>
        Task<IReadOnlyList<T>> GetAllAsync();

        /// <summary>
        /// Add a new entity to the repository
        /// </summary>
        /// <param name="entity">New entity</param>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Add a range of entities to the repository
        /// </summary>
        /// <param name="entities">Collection of entities</param>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Modify an existing entity
        /// </summary>
        /// <param name="entity">Modifiable entity</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Modify a range of existing entities
        /// </summary>
        /// <param name="entities">Collection of entities</param>
        Task UpdateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Modify an existing entity
        /// </summary>
        /// <param name="entity">Modifiable entity</param>

        /// <summary>
        /// Delete an existing entity
        /// </summary>
        /// <param name="entity">To be deleted entity</param>
        void Delete(T entity);

        /// <summary>
        /// Delete an existing entity
        /// </summary>
        /// <param name="entity">Id of the entity</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Remove a range of existing entity
        /// </summary>
        /// <param name="entity">To be deleted entities</param>
        void DeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// Remove a range of existing entity
        /// </summary>
        /// <param name="entity">Id of entities</param>
        Task DeleteRangeAsync(IEnumerable<Guid> ids);
    }
}