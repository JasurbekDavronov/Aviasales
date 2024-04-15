using Aviasales.API.DTOs.Flight;

namespace Aviasales.API.Models
{
    public class Flight
    {
        public required string FlightId;
        public int Capacity;
        public int AvailableSeats;
        public string? Origin;
        public string? Destination;
        public DateTime? DepartureTime;
        public DateTime? ArrivalTime;
    }
}
