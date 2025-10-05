using OptifyBookingTask.Domain.Common;

namespace OptifyBookingTask.Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false);
        Task<TEntity?> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
