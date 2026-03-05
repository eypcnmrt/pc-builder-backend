using PcBuilderBackend.Domain.Interfaces;
using PcBuilderBackend.Persistence.Contexts;
using PcBuilderBackend.Persistence.Repositories;

namespace PcBuilderBackend.Persistence.UnitOfWork
{
    public class EFCoreUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public EFCoreUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class, IEntity =>
            new EFCoreRepository<T>(_context);

        public int SaveChanges() =>
            _context.SaveChanges();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            _context.SaveChangesAsync(cancellationToken);
    }
}
