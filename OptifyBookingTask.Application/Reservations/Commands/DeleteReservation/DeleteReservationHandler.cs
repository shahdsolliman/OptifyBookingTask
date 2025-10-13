using MediatR;
using OptifyBookingTask.Domain.Contracts;
using OptifyBookingTask.Domain.Entities;

namespace Application.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationHandler : IRequestHandler<DeleteReservationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReservationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Reservation, int>();
            var reservation = await repo.GetByIdAsync(request.Id);

            if (reservation is null)
                throw new KeyNotFoundException($"Reservation with Id {request.Id} not found.");

            repo.Delete(reservation);
            await _unitOfWork.CompleteAsync();

            return Unit.Value; 
        }
    }
}
