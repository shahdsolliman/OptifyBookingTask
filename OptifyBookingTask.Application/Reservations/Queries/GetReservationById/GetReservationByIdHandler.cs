using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var reservation = await _unitOfWork.GetRepository<Reservation, int>()
                .Query()
                .Include(r => r.Trip)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (reservation == null)
                throw new KeyNotFoundException($"Reservation with Id {request.Id} not found.");

            return _mapper.Map<ReservationToReturnDto>(reservation);
        }
    }
}
