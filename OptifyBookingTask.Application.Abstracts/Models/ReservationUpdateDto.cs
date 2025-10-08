using System.ComponentModel.DataAnnotations;

namespace OptifyBookingTask.Application.Abstracts.Models
{
    /// <summary>
    /// DTO used to update an existing reservation.
    /// </summary>
    public record ReservationUpdateDto
    {
        /// <summary>
        /// The updated trip ID (if the trip is changed).
        /// </summary>

        [Required]
        public int TripId { get; set; }

        /// <summary>
        /// The updated customer name.
        /// </summary>
        [Required] 
        public required string CustomerName { get; set; }

        /// <summary>
        /// The updated reservation date.
        /// </summary>

        [Required] 
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Updated notes (if any).
        /// </summary>
        public string? Notes { get; set; }
    }
}
