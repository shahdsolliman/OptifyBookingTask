namespace OptifyBookingTask.Domain.Common
{
    /// <summary>
    /// Base entity with generic primary key.
    /// </summary>
    public abstract class BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public required TKey Id { get; set; }
    }
}
