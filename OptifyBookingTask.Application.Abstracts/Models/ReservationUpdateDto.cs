namespace OptifyBookingTask.Application.Abstracts.Models
{
    /// <summary>
    /// DTO used to update an existing reservation.
    /// </summary>
    public record ReservationUpdateDto
    {
        /// <summary>
        /// The unique ID of the reservation to update.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The updated trip ID (if the trip is changed).
        /// </summary>
        public int TripId { get; set; }

        /// <summary>
        /// The updated customer name.
        /// </summary>
        public required string CustomerName { get; set; }

        /// <summary>
        /// The updated reservation date.
        /// </summary>
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Updated notes (if any).
        /// </summary>
        public string? Notes { get; set; }
    }
}
