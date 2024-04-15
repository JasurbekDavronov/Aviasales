using Aviasales.API.DTOs.Flight;
using Aviasales.API.Models;

namespace Aviasales.API.Extensions
{
    public static class FlightExtension
    {
        public static FlightResponse? ToFlightResponse(this Flight? flight)
        {
            if (flight is null)
            {
                return null;
            }

            return new FlightResponse
            {
                FlightId = flight.FlightId,
                Origin = flight.Origin,
                Destination = flight.Destination,
                ArrivalTime = flight.ArrivalTime,
                DepartureTime = flight.DepartureTime,
                Capacity = flight.Capacity,
                AvailableSeats = flight.AvailableSeats
            };
        }
    }
}
