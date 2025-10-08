using Microsoft.AspNetCore.Mvc;
using OptifyBookingTask.API.Common;
using OptifyBookingTask.API.Controllers.Errors;
using OptifyBookingTask.Application.Abstracts.Models;
using OptifyBookingTask.Application.Abstracts.Services;
using OptifyBookingTask.Application.Exceptions;

namespace OptifyBookingTask.API.Controllers
{
    public class ReservationsController : BaseApiController
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Get all reservations.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/reservations
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReservationToReturnDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservationToReturnDto>>> GetAll()
        {
            var reservations = await _reservationService.GetReservationsAsync();
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
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ReservationToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationToReturnDto>> GetById(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                throw new NotFoundException(nameof(ReservationToReturnDto), id);

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

            var createdReservation = await _reservationService.CreateReservationAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdReservation.Id }, createdReservation);
        }

        /// <summary>
        /// Update an existing reservation.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <param name="dto">Updated reservation data</param>
        /// <returns>Updated reservation</returns>
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

            var updatedReservation = await _reservationService.UpdateReservationAsync(id, dto);
            if (updatedReservation == null)
                throw new NotFoundException(nameof(ReservationToReturnDto), id);

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
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _reservationService.DeleteReservationAsync(id);
            if (!deleted)
                throw new NotFoundException(nameof(ReservationToReturnDto), id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Reservation deleted successfully."));
        }

    }
}
