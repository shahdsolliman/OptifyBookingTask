using MediatR;
using OptifyBookingTask.Application.Abstracts.Models;

namespace Application.Reservations.Commands.UpdateReservation
{
    public record UpdateReservationCommand(int Id, ReservationUpdateDto ReservationDto) : IRequest<ReservationToReturnDto>;
}
