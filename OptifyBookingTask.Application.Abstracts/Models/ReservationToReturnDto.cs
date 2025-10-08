namespace OptifyBookingTask.Application.Abstracts.Models
{
    /// <summary>
    /// DTO returned to the client after reservation creation, retrieval, or update.
    /// </summary>
    public class ReservationToReturnDto
    {
        /// <summary>
        /// Reservation unique identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Customer name for the reservation.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Name of the related trip.
        /// </summary>
        public string TripName { get; set; } = string.Empty;

        /// <summary>
        /// City of the trip.
        /// </summary>
        public string CityName { get; set; } = string.Empty;

        /// <summary>
        /// Date when the reservation is scheduled.
        /// </summary>
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Date when the reservation was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Additional notes for the reservation.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Email of the user who created the reservation.
        /// </summary>
        public string UserEmail { get; set; } = string.Empty;
    }
}
