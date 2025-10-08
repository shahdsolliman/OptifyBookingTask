using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OptifyBookingTask.Application.Abstracts.Models;
using OptifyBookingTask.Application.Abstracts.Services;
using OptifyBookingTask.Application.Exceptions;
using OptifyBookingTask.Domain.Contracts;
using OptifyBookingTask.Domain.Entities;

namespace OptifyBookingTask.Application.Services
{
    internal class TripService : ITripService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TripService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all trips.
        /// </summary>
        public async Task<IEnumerable<TripDto>> GetTripsAsync()
        {
            var trips = await _unitOfWork.GetRepository<Trip, int>()
                .Query()
                .ToListAsync();

            return _mapper.Map<IEnumerable<TripDto>>(trips);
        }

        /// <summary>
        /// Get a single trip by ID.
        /// </summary>
        public async Task<TripDto> GetTripByIdAsync(int id)
        {
            var trip = await _unitOfWork.GetRepository<Trip, int>()
                .GetByIdAsync(id);

            if (trip == null)
                throw new NotFoundException(nameof(TripDto), id);

            return _mapper.Map<TripDto>(trip);
        }
    }
}
