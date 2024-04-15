namespace Aviasales.API.DTOs.Flight
{
    public class FlightResponse
    {
        public required string FlightId { get; set; }
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }

        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
    }
}
