using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OptifyBookingTask.Application.Abstracts.Models;
using OptifyBookingTask.Domain.Contracts;
using OptifyBookingTask.Domain.Entities;

namespace Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationHandler : IRequestHandler<CreateReservationCommand, ReservationToReturnDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateReservationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReservationToReturnDto> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = _mapper.Map<Reservation>(request.ReservationDto);

            await _unitOfWork.GetRepository<Reservation, int>().AddAsync(reservation);

            await _unitOfWork.CompleteAsync();

            // Reload the reservation including Trip and User navigation properties
            var createdReservation = await _unitOfWork.GetRepository<Reservation, int>()
                .Query()
                .Include(r => r.Trip)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reservation.Id, cancellationToken);

            if (createdReservation == null)
                throw new KeyNotFoundException($"Reservation with Id {reservation.Id} was not found after creation.");

            return _mapper.Map<ReservationToReturnDto>(createdReservation);
        }
    }
}
