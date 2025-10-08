using Microsoft.AspNetCore.Mvc;
using OptifyBookingTask.API.Common;
using OptifyBookingTask.API.Controllers.Errors;
using OptifyBookingTask.Application.Abstracts.Models;
using OptifyBookingTask.Application.Abstracts.Services;
using OptifyBookingTask.Application.Exceptions;

namespace OptifyBookingTask.API.Controllers
{
    public class TripsController : BaseApiController
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        /// <summary>
        /// Get all trips.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/trips
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TripDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TripDto>>> GetAll()
        {
            var trips = await _tripService.GetTripsAsync();
            return Ok(trips);
        }

        /// <summary>
        /// Get a single trip by ID.
        /// </summary>
        /// <param name="id">Trip ID</param>
        /// <remarks>
        /// Sample request:
        /// GET /api/trips/1
        /// </remarks>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TripDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TripDto>> GetById(int id)
        {
            var trip = await _tripService.GetTripByIdAsync(id);
            if (trip == null)
                throw new NotFoundException(nameof(TripDto), id);

            return Ok(trip);
        }
    }
}
