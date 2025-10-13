using Application.Reservations.Commands.CreateReservation;
using Application.Reservations.Commands.DeleteReservation;
using Application.Reservations.Commands.UpdateReservation;
using Application.Reservations.Queries.GetAllReservations;
using Application.Reservations.Queries.GetReservationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OptifyBookingTask.API.Common;
using OptifyBookingTask.API.Controllers.Errors;
using OptifyBookingTask.Application.Abstracts.Models;

namespace OptifyBookingTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ReservationsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all reservations.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/reservations
        /// </remarks>
        /// <returns>List of reservations</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReservationToReturnDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservationToReturnDto>>> GetAll()
        {
            var query = new GetAllReservationsQuery();
            var reservations = await _mediator.Send(query);
            return Ok(reservations);
        }

        /// <summary>
        /// Get a reservation by ID.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <remarks>
        /// Sample request:
        /// GET /api/reservations/1
        /// </remarks>
        /// <returns>Reservation details</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ReservationToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationToReturnDto>> GetById(int id)
        {
            var query = new GetReservationByIdQuery(id);
            var reservation = await _mediator.Send(query);

            if (reservation == null)
                return NotFound(new ApiResponse(404, $"Reservation with id {id} not found"));

            return Ok(reservation);
        }

        /// <summary>
        /// Create a new reservation.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/reservations
        /// {
        ///    "customerName": "Ahmed",
        ///    "tripId": 1,
        ///    "reservationDate": "2025-10-08T12:00:00",
        ///    "notes": "Some notes"
        /// }
        /// </remarks>
        /// <param name="dto">Reservation create DTO</param>
        /// <returns>Created reservation</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ReservationToReturnDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReservationToReturnDto>> Create([FromBody] ReservationCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationErrorResponse("Validation error")
                {
                    Errors = ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .Select(e => new ApiValidationErrorResponse.ValidationError
                        {
                            Field = e.Key,
                            Error = e.Value!.Errors.Select(x => x.ErrorMessage)
                        })
                });

            var command = new CreateReservationCommand(dto);
            var createdReservation = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = createdReservation.Id }, createdReservation);
        }

        /// <summary>
        /// Update an existing reservation.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <param name="dto">Updated reservation data</param>
        /// <remarks>
        /// Sample request:
        /// PUT /api/reservations/1
        /// {
        ///    "customerName": "Ahmed Updated",
        ///    "tripId": 1,
        ///    "reservationDate": "2025-10-09T12:00:00",
        ///    "notes": "Updated notes"
        /// }
        /// </remarks>
        /// <returns>Updated reservation</returns>
        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ReservationToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReservationToReturnDto>> Update(int id, [FromBody] ReservationUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationErrorResponse("Validation error")
                {
                    Errors = ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .Select(e => new ApiValidationErrorResponse.ValidationError
                        {
                            Field = e.Key,
                            Error = e.Value!.Errors.Select(x => x.ErrorMessage)
                        })
                });

            var command = new UpdateReservationCommand(id, dto);
            var updatedReservation = await _mediator.Send(command);

            if (updatedReservation == null)
                return NotFound(new ApiResponse(404, $"Reservation with id {id} not found"));

            return Ok(updatedReservation);
        }

        /// <summary>
        /// Delete a reservation by ID.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/reservations/1
        /// </remarks>
        /// <returns>Delete result</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteReservationCommand(id);
            await _mediator.Send(command);

            return Ok(new ApiResponse(200, "Reservation deleted successfully."));
        }
    }
}
