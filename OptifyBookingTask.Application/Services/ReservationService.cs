using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OptifyBookingTask.Application.Abstracts.Models;
using OptifyBookingTask.Application.Abstracts.Services;
using OptifyBookingTask.Application.Exceptions;
using OptifyBookingTask.Domain.Contracts;
using OptifyBookingTask.Domain.Entities;

namespace OptifyBookingTask.Application.Services
{
    internal class ReservationService(IUnitOfWork _unitOfWork, IMapper _mapper) : IReservationService
    {
        /// <summary>
        /// Get all reservations with related Trip and User data.
        /// </summary>
        public async Task<IEnumerable<ReservationToReturnDto>> GetReservationsAsync()
        {
            var reservations = await _unitOfWork.GetRepository<Reservation, int>()
                .Query()                        // Queryable to allow Includes
                .Include(r => r.Trip)           // Include Trip data
                .Include(r => r.User)           // Include User data
                .ToListAsync();

            return _mapper.Map<IEnumerable<ReservationToReturnDto>>(reservations);
        }

        /// <summary>
        /// Get a single reservation by its ID, including related Trip and User.
        /// </summary>
        public async Task<ReservationToReturnDto> GetReservationByIdAsync(int id)
        {
            var reservation = await _unitOfWork.GetRepository<Reservation, int>()
                .Query()
                .Include(r => r.Trip)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return null;

            return _mapper.Map<ReservationToReturnDto>(reservation);
        }

        /// <summary>
        /// Create a new reservation and return it with related Trip and User info.
        /// </summary>
        public async Task<ReservationToReturnDto> CreateReservationAsync(ReservationCreateDto dto)
        {
            // Map DTO to entity
            var reservation = _mapper.Map<Reservation>(dto);

            // Add to repository and save
            await _unitOfWork.GetRepository<Reservation, int>().AddAsync(reservation);
            await _unitOfWork.CompleteAsync();

            // Reload the reservation including Trip and User
            var createdReservation = await _unitOfWork.GetRepository<Reservation, int>()
                .Query()
                .Include(r => r.Trip)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reservation.Id);

            return _mapper.Map<ReservationToReturnDto>(createdReservation);
        }

        /// <summary>
        /// Update an existing reservation by ID.
        /// </summary>
        public async Task<ReservationToReturnDto> UpdateReservationAsync(int id, ReservationUpdateDto dto)
        {
            var reservationRepo = _unitOfWork.GetRepository<Reservation, int>();
            var tripRepo = _unitOfWork.GetRepository<Trip, int>();

            // Check if reservation exists
            var existingReservation = await reservationRepo.GetByIdAsync(id);
            if (existingReservation == null)
                return null;

            // Validate Trip existence
            var tripExists = await tripRepo.GetByIdAsync(dto.TripId);
            if (tripExists == null)
                throw new BadRequestException($"Trip with ID {dto.TripId} does not exist.");

            // Map updated fields
            _mapper.Map(dto, existingReservation);
            reservationRepo.Update(existingReservation);
            await _unitOfWork.CompleteAsync();

            // Reload updated reservation including Trip and User
            var updatedReservation = await reservationRepo.Query()
                .Include(r => r.Trip)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<ReservationToReturnDto>(updatedReservation);
        }

        /// <summary>
        /// Delete a reservation by ID.
        /// </summary>
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
