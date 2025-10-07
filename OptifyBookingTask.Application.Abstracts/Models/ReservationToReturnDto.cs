namespace OptifyBookingTask.Application.Abstracts.Models
{
    /// <summary>
    /// DTO returned when a reservation is retrieved.
    /// </summary>
    public record ReservationToReturnDto
    {
        /// <summary>
        /// The reservation ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The customer name.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// The name of the trip.
        /// </summary>
        public string TripName { get; set; } = string.Empty;

        /// <summary>
        /// The name of the city where the trip takes place.
        /// </summary>
        public string CityName { get; set; } = string.Empty;

        /// <summary>
        /// The date of the reservation.
        /// </summary>
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// The date when the reservation was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Optional notes.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// The email of the user who made the reservation.
        /// </summary>
        public string UserEmail { get; set; } = string.Empty;
    }
}
