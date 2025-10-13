using AutoMapper;
using MediatR;
using OptifyBookingTask.Application.Abstracts.Models;
using OptifyBookingTask.Domain.Contracts;
using OptifyBookingTask.Domain.Entities;

namespace Application.Reservations.Queries.GetAllReservations
{
    public class GetAllReservationsHandler : IRequestHandler<GetAllReservationsQuery, IEnumerable<ReservationToReturnDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllReservationsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationToReturnDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Reservation, int>();
            var reservations = await repo.GetAllAsync();

            return _mapper.Map<IEnumerable<ReservationToReturnDto>>(reservations);
        }
    }
}
