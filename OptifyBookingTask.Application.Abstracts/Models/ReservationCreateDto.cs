using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        /// <summary>
        /// The ID of the trip to reserve.
        /// </summary>
        [Required(ErrorMessage = "TripId is required.")]
        public int TripId { get; set; }

        /// <summary>
        /// The customer's full name.
        /// </summary>
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name must not exceed 100 characters.")]
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// The date when the reservation is scheduled.
        /// </summary>
        [Required(ErrorMessage = "Reservation date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Optional notes provided by the user or admin.
        /// </summary>
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
    }
}
