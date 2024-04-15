using Aviasales.API.Models;

namespace Aviasales.API.Services.Interfaces
{
    public interface IFlightService
    {
        Task<Flight?> GetFlight(string flightId);

        Task<IEnumerable<Flight?>> GetAllFlights(bool onlyAvailable = false);

        Task<int> GetAvailableSeats(string flightId);

        Task<int> BookTheSeat(string flightId);

        Task<bool> UnbookTheSeat(string flightId);
    }
}
