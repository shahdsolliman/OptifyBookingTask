﻿using MediatR;

namespace Application.Reservations.Commands.DeleteReservation
{
    public record DeleteReservationCommand(int Id) : IRequest<Unit>;
}
