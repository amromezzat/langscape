using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly AppDbContext _dbContext;

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            T dbEntry = await GetByIdAsync(entity.Id);
            if (dbEntry != null) 
            {
                UpdateDbEntry(dbEntry, entity);
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            var updateTasks = new List<Task>();
            foreach (var entity in entities)
            {
                updateTasks.Add(UpdateAsync(entity));
            }
            await Task.WhenAll(updateTasks);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            T dbEntry = await _dbContext.Set<T>().FindAsync(id);
            Delete(dbEntry);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public async Task DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            ids = ids.ToArray();
            var dbEntries = await _dbContext.Set<T>().Where(entry => ids.Contains(entry.Id)).ToArrayAsync(); 
            DeleteRange(dbEntries);
        }

        private void UpdateDbEntry(T entry, T newEntry)
        {
            _dbContext.Entry(entry).CurrentValues.SetValues(newEntry);
        }
    }
}