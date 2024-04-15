using Aviasales.API.DTOs.Booking;
using Aviasales.API.Extensions;
using Aviasales.API.Models;
using Aviasales.API.Services.Interfaces;
using System.Collections.Concurrent;

namespace Aviasales.API.Services
{
    public class BookingService: IBookingService
    {
        private readonly ILogger<BookingService> _logger;
        private readonly IFlightService _flightService;

        private List<Booking> bookings = new List<Booking>()
        {
            new Booking
            {
                FlightId = "SAMTAS-202404140530",
                BookingId = Guid.NewGuid().ToString(),
                Name = "Jasur Davronov",
                SeatNumber = 44
            }
        };

        public BookingService(ILogger<BookingService> logger, IFlightService flightService)
        {
            _logger = logger;
            _flightService = flightService;
        }

        public async Task<Booking?> CreateBooking(BookingRequest booking)
        {
            var newBooking = new Booking
            {
                BookingId = Guid.NewGuid().ToString(),
                Name = booking.Name,
                FlightId = booking.FlightId
            };

            var seatNumber = await _flightService.BookTheSeat(booking.FlightId);

            if (seatNumber == 0)
            {
                return null;
            }

            newBooking.SeatNumber = seatNumber;
            bookings.Add(newBooking);

            return newBooking;
        }

        public async Task RemoveBooking(string bookingId)
        {
            var booking = await GetBooking(bookingId);

            if (booking != null)
            {
                bookings.Remove(booking);
            }
        }

        public async Task<Booking?> GetBooking(string bookingId)
        {
            return bookings.FirstOrDefault(b => b.BookingId == bookingId);
        }

        public async Task<IEnumerable<Booking?>> GetAllBookings()
        {
            return bookings.AsEnumerable();
        }
    }
}
