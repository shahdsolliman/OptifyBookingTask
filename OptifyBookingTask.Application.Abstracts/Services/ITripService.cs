using OptifyBookingTask.Application.Abstracts.Models;

namespace OptifyBookingTask.Application.Abstracts.Services
{
    public interface ITripService
    {
        Task<IEnumerable<TripDto>> GetTripsAsync();
        /// <summary>
        /// Get a trip by its identifier. Throws NotFoundException if the trip does not exist.
        /// </summary>
        Task<TripDto> GetTripByIdAsync(int id);
    }
}
