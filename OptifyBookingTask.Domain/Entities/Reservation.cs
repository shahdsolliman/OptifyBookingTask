using OptifyBookingTask.Domain.Common;

namespace OptifyBookingTask.Domain.Entities
{
    /// <summary>
    /// Represents a reservation made by a user for a specific trip.
    /// </summary>
    public class Reservation : BaseEntity<int>
    {
        /// <summary>
        /// The ID of the user who created the reservation.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The user who created the reservation.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Name of the customer for whom the reservation was made.
        /// </summary>
        public required string CustomerName { get; set; }

        /// <summary>
        /// The ID of the trip being reserved.
        /// </summary>
        public int TripId { get; set; }

        /// <summary>
        /// The trip details.
        /// </summary>
        public Trip Trip { get; set; } = null!;

        /// <summary>
        /// Date when the reservation is scheduled.
        /// </summary>
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Date when the reservation was created in the system.
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Optional notes provided by the customer or admin.
        /// </summary>
        public string? Notes { get; set; }
    }
}
