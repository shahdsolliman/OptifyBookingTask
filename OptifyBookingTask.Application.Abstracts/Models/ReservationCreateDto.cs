namespace OptifyBookingTask.Application.Abstracts.Models
{
    /// <summary>
    /// DTO used to create a new reservation.
    /// </summary>
    public record ReservationCreateDto
    {
        /// <summary>
        /// The ID of the user creating the reservation.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The ID of the trip to reserve.
        /// </summary>
        public int TripId { get; set; }

        /// <summary>
        /// The customer's full name.
        /// </summary>
        public required string CustomerName { get; set; }

        /// <summary>
        /// The date when the reservation is scheduled.
        /// </summary>
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Optional notes provided by the user or admin.
        /// </summary>
        public string? Notes { get; set; }
    }
}
