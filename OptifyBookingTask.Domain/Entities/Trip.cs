using OptifyBookingTask.Domain.Common;

namespace OptifyBookingTask.Domain.Entities
{
    public class Trip : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required string CityName { get; set; }
        public decimal Price { get; set; }
        public required string ImageUrl { get; set; }
        public required string Content { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    }
}
