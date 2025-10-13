using AutoMapper;
using MediatR;
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

            return _mapper.Map<ReservationToReturnDto>(reservation);
        }
    }
}
