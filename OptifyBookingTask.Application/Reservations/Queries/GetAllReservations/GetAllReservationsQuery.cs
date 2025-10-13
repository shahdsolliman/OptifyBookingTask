using MediatR;
using OptifyBookingTask.Application.Abstracts.Models;

namespace Application.Reservations.Queries.GetAllReservations
{
    public record GetAllReservationsQuery() : IRequest<IEnumerable<ReservationToReturnDto>>;
}
