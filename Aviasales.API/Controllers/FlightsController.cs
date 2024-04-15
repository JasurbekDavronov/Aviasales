using Aviasales.API.DTOs.Flight;
using Aviasales.API.Extensions;
using Aviasales.API.Services;
using Aviasales.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aviasales.API.Controllers
{
    [ApiController]
    [Route("api/flights")]
    public class FlightsController : ControllerBase
    {
        private readonly ILogger<FlightsController> _logger;
        private readonly IFlightService _flightService;

        public FlightsController(ILogger<FlightsController> logger, IFlightService flightService)
        {
            _logger = logger;
            _flightService = flightService;
        }

        [Authorize]
        [HttpGet("{flightId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FlightResponse>> GetFlight(string flightId)
        {
            _logger.LogInformation($"Calling: {nameof(GetFlight)}");

            var flight = await _flightService.GetFlight(flightId);

            if (flight == null) 
            {
                return NotFound();
            }

            var response = flight.ToFlightResponse();

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FlightResponse>>> GetAllFlights(bool onlyNotBooked)
        {
            _logger.LogInformation($"Calling: {nameof(GetAllFlights)}");

            var flights = await _flightService.GetAllFlights(onlyNotBooked);

            var response = flights.Select(f => f.ToFlightResponse());

            return Ok(response);
        }
    }
}
