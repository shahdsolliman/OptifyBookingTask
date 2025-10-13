using MediatR;
using OptifyBookingTask.Application.Abstracts.Models;

namespace Application.Reservations.Queries.GetReservationById
{
    public record GetReservationByIdQuery(int Id) : IRequest<ReservationToReturnDto>;
}
