namespace OptifyBookingTask.Application.Abstracts.Models
{
    /// <summary>
    /// Data Transfer Object representing a trip.
    /// Used to return trip information in API responses or UI views.
    /// </summary>
    public record TripDto
    {
        /// <summary>
        /// The unique identifier of the trip.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Name of the trip.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// City where the trip takes place.
        /// </summary>
        public string CityName { get; init; } = string.Empty;

        /// <summary>
        /// Price of the trip.
        /// </summary>
        public decimal Price { get; init; }

        /// <summary>
        /// URL to an image representing the trip.
        /// </summary>
        public string ImageUrl { get; init; } = string.Empty;

        /// <summary>
        /// Description or content of the trip. HTML format is allowed.
        /// </summary>
        public string Content { get; init; } = string.Empty;

        /// <summary>
        /// Date when the trip was created.
        /// </summary>
        public DateTime CreationDate { get; init; }
    }
}
