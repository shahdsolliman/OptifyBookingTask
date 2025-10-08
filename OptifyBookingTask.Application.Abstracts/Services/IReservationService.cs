
using OptifyBookingTask.Application.Abstracts.Models;

namespace OptifyBookingTask.Application.Abstracts.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationToReturnDto>> GetReservationsAsync();
        Task<ReservationToReturnDto> GetReservationByIdAsync(int id);
        Task<ReservationToReturnDto> CreateReservationAsync(ReservationCreateDto dto);
        Task<ReservationToReturnDto> UpdateReservationAsync(int id, ReservationUpdateDto dto);
        Task<bool> DeleteReservationAsync(int id);
    }
}
