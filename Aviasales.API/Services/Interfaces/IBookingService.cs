using Aviasales.API.DTOs.Booking;
using Aviasales.API.Models;

namespace Aviasales.API.Services.Interfaces
{
    public interface IBookingService
    {
        Task<Booking?> CreateBooking(BookingRequest booking);
        
        Task RemoveBooking(string bookingId);

        Task<Booking?> GetBooking(string bookingId);

        Task<IEnumerable<Booking?>> GetAllBookings();
    }
}
