namespace Aviasales.API.DTOs.Booking
{
    public class BookingResponse
    {
        public required string FlightId {  get; set; }
        public required string BookId { get; set; }
        public required string Name { get; set; }
        public int SeatNumber { get; set; }
    }
}
