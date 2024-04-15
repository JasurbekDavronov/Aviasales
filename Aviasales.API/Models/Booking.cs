using Aviasales.API.DTOs.Flight;

namespace Aviasales.API.Models
{
    public class Booking
    {
        public required string FlightId;
        public required string BookingId;
        public required string Name;
        public int SeatNumber;
    }
}
