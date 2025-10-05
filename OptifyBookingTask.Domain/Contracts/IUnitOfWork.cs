using OptifyBookingTask.Domain.Common;

namespace OptifyBookingTask.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>;
        Task<int> CompleteAsync();
    }
}
