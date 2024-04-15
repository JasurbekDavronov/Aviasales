using Aviasales.API.DTOs.Booking;
using Aviasales.API.Models;

namespace Aviasales.API.Extensions
{
    public static class BookingExtension
    {
        public static BookingResponse? ToBookingResponse(this Booking? booking)
        {
            if (booking is null)
            {
                return null;
            }

            return new BookingResponse
            {
                BookId = booking.BookingId,
                Name = booking.Name,
                FlightId = booking.FlightId,
                SeatNumber = booking.SeatNumber
            };
        }
    }
}
