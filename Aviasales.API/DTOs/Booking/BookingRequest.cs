namespace Aviasales.API.DTOs.Booking
{
    public class BookingRequest
    {
        public required string FlightId { get; set; }
        public required string Name { get; set; }
    }
}
