using Aviasales.API.DTOs.Booking;
using Aviasales.API.DTOs.Flight;
using Aviasales.API.Extensions;
using Aviasales.API.Models;
using Aviasales.API.Services;
using Aviasales.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aviasales.API.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingService _bookingService;

        public BookingController(ILogger<BookingController> logger, IBookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookingResponse>> BookTheFlight(BookingRequest booking)
        {
            _logger.LogInformation($"Calling: {nameof(BookTheFlight)}");

            var createdBooking = await _bookingService.CreateBooking(booking);

            if (createdBooking == null)
            {
                return BadRequest();
            }

            var response = createdBooking.ToBookingResponse();

            return Created(new Uri($"{Request.Path}{booking.FlightId}", UriKind.Relative), response);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BookingResponse>>> GetAllBookings()
        {
            _logger.LogInformation($"Calling: {nameof(GetAllBookings)}");

            var bookings = await _bookingService.GetAllBookings();

            var response = bookings.Select( b => b.ToBookingResponse());

            return Ok(response);
        }
    }
}
