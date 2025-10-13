using AutoMapper;
using MediatR;
using OptifyBookingTask.Application.Abstracts.Models;
using OptifyBookingTask.Domain.Contracts;
using OptifyBookingTask.Domain.Entities;

namespace Application.Reservations.Queries.GetReservationById
{
    public class GetReservationByIdHandler : IRequestHandler<GetReservationByIdQuery, ReservationToReturnDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReservationByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReservationToReturnDto> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Reservation, int>();
            var reservation = await repo.GetByIdAsync(request.Id);

            if (reservation == null)
                throw new KeyNotFoundException($"Reservation with Id {request.Id} not found.");

            return _mapper.Map<ReservationToReturnDto>(reservation);
        }
    }
}
