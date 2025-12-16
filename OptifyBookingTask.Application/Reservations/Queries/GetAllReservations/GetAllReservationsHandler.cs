using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var reservations = await _unitOfWork.GetRepository<Reservation, int>()
                .Query()
                .Include(r => r.Trip)
                .Include(r => r.User)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ReservationToReturnDto>>(reservations);
        }
    }
}
