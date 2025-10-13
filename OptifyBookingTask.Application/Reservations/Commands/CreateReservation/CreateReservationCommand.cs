using MediatR;
using OptifyBookingTask.Application.Abstracts.Models;

namespace Application.Reservations.Commands.CreateReservation
{
    public record CreateReservationCommand(ReservationCreateDto ReservationDto) : IRequest<ReservationToReturnDto>;
}
