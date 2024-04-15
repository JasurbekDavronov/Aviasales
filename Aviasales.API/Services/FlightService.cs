using Aviasales.API.DTOs.Flight;
using Aviasales.API.Extensions;
using Aviasales.API.Models;
using Aviasales.API.Services.Interfaces;
using System.Collections.Concurrent;

namespace Aviasales.API.Services
{
    public class FlightService: IFlightService
    {
        private readonly ILogger<FlightService> _logger;

        public ConcurrentBag<Flight> flights = new ConcurrentBag<Flight>()
        {
            new Flight
            {
                FlightId = "SAMTAS-202404140530",
                Origin = "SAMARKAND",
                Destination = "TASHKENT",
                Capacity = 200,
                AvailableSeats = 200,
                DepartureTime = new DateTime(2024, 04, 14, 05, 30, 00, DateTimeKind.Utc),
                ArrivalTime = new DateTime(2024, 04, 14, 07, 00, 00, DateTimeKind.Utc),
            },
            new Flight
            {
                FlightId = "TASSAM-202404141400",
                Origin = "TASHKENT",
                Destination = "SAMARKAND",
                Capacity = 150,
                AvailableSeats = 150,
                DepartureTime = new DateTime(2024, 04, 14, 14, 00, 00, DateTimeKind.Utc),
                ArrivalTime = new DateTime(2024, 04, 14, 15, 30, 00, DateTimeKind.Utc),
            },
            new Flight
            {
                FlightId = "ANDBUH-202404150830",
                Origin = "ANDIJAN",
                Destination = "BUKHARA",
                Capacity = 150,
                AvailableSeats = 150,
                DepartureTime = new DateTime(2024, 04, 14, 08, 30, 00, DateTimeKind.Utc),
                ArrivalTime = new DateTime(2024, 04, 14, 11, 30, 00, DateTimeKind.Utc),
            },
        };

        public FlightService(ILogger<FlightService> logger)
        {
            _logger = logger;
        }

        public async Task<Flight?> GetFlight(string flightId)
        {
            return flights.FirstOrDefault(f => f.FlightId == flightId);
        }

        public async Task<IEnumerable<Flight?>> GetAllFlights(bool onlyAvailable = false)
        {
            if (onlyAvailable)
            {
                return flights
                    .Where(f => f.AvailableSeats > 0)
                    .AsEnumerable();
            }

            return flights.AsEnumerable();
        }

        public async Task<int> GetAvailableSeats(string flightId)
        {
            var flight = await GetFlight(flightId);
            
            if (flight != null) 
            {
                return flight.AvailableSeats;
            }

            return 0;
        }

        public async Task<int> BookTheSeat(string flightId)
        {
            var availableSeats = await GetAvailableSeats(flightId);

            if (availableSeats > 0)
            {
                var flight = flights
                    .Where(f => f.FlightId == flightId)
                    .FirstOrDefault();

                flight.AvailableSeats -= 1;
                return Random.Shared.Next(flight.Capacity);
            }

            return 0;
        }

        public async Task<bool> UnbookTheSeat(string flightId)
        {
            var flight = flights
                    .Where(f => f.FlightId == flightId)
                    .FirstOrDefault();

            if (flight != null)
            {
                flight.AvailableSeats += 1;
                return true;
            }

            return false;
        }
    }
}
