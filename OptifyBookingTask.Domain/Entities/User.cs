using OptifyBookingTask.Domain.Common;

namespace OptifyBookingTask.Domain.Entities
{
    public class User : BaseEntity<int>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
