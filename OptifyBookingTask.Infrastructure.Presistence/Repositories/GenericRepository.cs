using Microsoft.EntityFrameworkCore;
using OptifyBookingTask.Domain.Common;
using OptifyBookingTask.Domain.Contracts;
using OptifyBookingTask.Infrastructure.Presistence.Data;

namespace OptifyBookingTask.Infrastructure.Presistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(BookingDbContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
            => withTracking ? await _dbContext.Set<TEntity>().ToListAsync() :
            await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();


        public async Task<TEntity?> GetByIdAsync(TKey id) 
            => await _dbContext.Set<TEntity>().FindAsync(id);


        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);

        public IQueryable<TEntity> Query(bool withTracking = false)
        {
            return withTracking
                ? _dbContext.Set<TEntity>()
                : _dbContext.Set<TEntity>().AsNoTracking();
        }


    }
}
