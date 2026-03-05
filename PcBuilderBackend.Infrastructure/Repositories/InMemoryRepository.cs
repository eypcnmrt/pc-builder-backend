using System.Linq.Expressions;
using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Infrastructure.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly List<T> _store;

        public InMemoryRepository(List<T> seedData)
        {
            _store = seedData;
        }

        public IQueryable<T> AsQueryable() => _store.AsQueryable();

        public Task<T?> GetByIdAsync(int id, CancellationToken ct = default) =>
            Task.FromResult(_store.FirstOrDefault(e => e.Id == id));

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default) =>
            Task.FromResult(_store.AsQueryable().Any(predicate));

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default) =>
            Task.FromResult(_store.AsQueryable().FirstOrDefault(predicate));

        public Task<(List<T> Items, int TotalCount)> GetPagedAsync(IQueryable<T> query, int skip, int take, CancellationToken ct = default)
        {
            var totalCount = query.Count();
            var items = query.Skip(skip).Take(take).ToList();
            return Task.FromResult((items, totalCount));
        }

        public Task AddAsync(T entity, CancellationToken ct = default)
        {
            _store.Add(entity);
            return Task.CompletedTask;
        }

        public void Update(T entity)
        {
            var index = _store.FindIndex(e => e.Id == entity.Id);
            if (index >= 0) _store[index] = entity;
        }

        public void Delete(T entity) => _store.Remove(entity);
    }
}
