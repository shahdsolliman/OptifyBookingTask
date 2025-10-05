using OptifyBookingTask.Domain.Common;
using OptifyBookingTask.Domain.Contracts;
using OptifyBookingTask.Infrastructure.Presistence.Data;
using OptifyBookingTask.Infrastructure.Presistence.Repositories;
using System.Collections.Concurrent;

namespace OptifyBookingTask.Infrastructure.Presistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingDbContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(BookingDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return (IGenericRepository<TEntity,TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));
        }

        public async Task<int> CompleteAsync()
            => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();
    }
}
