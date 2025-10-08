namespace OptifyBookingTask.Application.Abstracts.Models
{
    // A general DTO for displaying all reservation details
    public record ReservationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public int TripId { get; set; }
        public string TripName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime ReservationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Notes { get; set; }
    }
}
