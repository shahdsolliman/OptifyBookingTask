using System.ComponentModel.DataAnnotations;

namespace OptifyBookingTask.Application.Abstracts.Models
{
    /// <summary>
    /// DTO used to update an existing reservation.
    /// </summary>
    public record ReservationUpdateDto
    {
        [Required(ErrorMessage = "TripId is required.")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name must not exceed 100 characters.")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Reservation date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime ReservationDate { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
    }
}
