using AutoMapper;
using OptifyBookingTask.Application.Abstracts.Models;
using OptifyBookingTask.Application.Abstracts.Services;
using OptifyBookingTask.Domain.Contracts;
using OptifyBookingTask.Domain.Entities;

namespace OptifyBookingTask.Application.Services
{
    internal class ReservationService(IUnitOfWork _unitOfWork, IMapper _mapper) : IReservationService
    {

        public async Task<IEnumerable<ReservationToReturnDto>> GetReservationsAsync()
        {
            var reservations = await _unitOfWork.GetRepository<Reservation, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ReservationToReturnDto>>(reservations);
        }

        public async Task<ReservationToReturnDto> GetReservationByIdAsync(int id)
        {
            var reservation = await _unitOfWork.GetRepository<Reservation, int>().GetByIdAsync(id);
            if (reservation == null)
                return null;
            return _mapper.Map<ReservationToReturnDto>(reservation);
        }

        public async Task<ReservationToReturnDto> CreateReservationAsync(ReservationCreateDto dto)
        {
            var reservation = _mapper.Map<Reservation>(dto);
            await _unitOfWork.GetRepository<Reservation, int>().AddAsync(reservation);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ReservationToReturnDto>(reservation);
        }

        public async Task<ReservationToReturnDto> UpdateReservationAsync(ReservationUpdateDto dto)
        {
            var repo = _unitOfWork.GetRepository<Reservation, int>();
            var existingReservation = await repo.GetByIdAsync(dto.Id);
            if (existingReservation == null)
                return null;

            // Map updated fields
            _mapper.Map(dto, existingReservation);

            repo.Update(existingReservation);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ReservationToReturnDto>(existingReservation);
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Reservation, int>();
            var reservation = await repo.GetByIdAsync(id);
            if (reservation == null)
                return false;

            repo.Delete(reservation);
            await _unitOfWork.CompleteAsync();
            return true;
        }

    }
}
