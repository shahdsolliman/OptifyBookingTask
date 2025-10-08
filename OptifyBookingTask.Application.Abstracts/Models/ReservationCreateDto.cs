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
        [Required] 
        public int UserId { get; set; }

        /// <summary>
        /// The ID of the trip to reserve.
        /// </summary>
        [Required] 
        public int TripId { get; set; }

        /// <summary>
        /// The customer's full name.
        /// </summary>
        [Required] 
        public string CustomerName { get; set; }

        /// <summary>
        /// The date when the reservation is scheduled.
        /// </summary>
        [Required] 
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Optional notes provided by the user or admin.
        /// </summary>
        public string? Notes { get; set; }
    }
}
