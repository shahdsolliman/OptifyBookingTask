using OptifyBookingTask.Application.Abstracts.Models;

namespace OptifyBookingTask.Application.Abstracts.Services
{
    public interface ITripService
    {
        Task<IEnumerable<TripDto>> GetTripsAsync();
        Task<TripDto?> GetTripByIdAsync(int id);
    }
}
