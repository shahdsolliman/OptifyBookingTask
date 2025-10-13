using AutoMapper;
using MediatR;
using OptifyBookingTask.Application.Abstracts.Models;
using OptifyBookingTask.Domain.Contracts;
using OptifyBookingTask.Domain.Entities;

namespace Application.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationHandler : IRequestHandler<UpdateReservationCommand, ReservationToReturnDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReservationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReservationToReturnDto> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Reservation, int>();
            var reservation = await repo.GetByIdAsync(request.Id);
            if (reservation is null)
                throw new KeyNotFoundException($"Reservation with Id {request.Id} not found.");

            _mapper.Map(request.ReservationDto, reservation);

            repo.Update(reservation);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ReservationToReturnDto>(reservation);
        }
    }
}
