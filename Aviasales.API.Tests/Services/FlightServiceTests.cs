using Aviasales.API.Models;
using Aviasales.API.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Aviasales.API.Services.Tests
{
    public class FlightServiceTests
    {
        private readonly IFlightService _flightService;
        private readonly Mock<IFlightService> _flightServiceMock;

        public FlightServiceTests()
        {
            _flightServiceMock = new Mock<IFlightService>();
            _flightServiceMock.Setup(service => service.GetFlight("SAMTAS-202404140530"))
                .ReturnsAsync(new Flight
                {
                    FlightId = "SAMTAS-202404140530",
                    Origin = "SAMARKAND",
                    Destination = "TASHKENT",
                    Capacity = 200,
                    AvailableSeats = 200,
                    DepartureTime = new DateTime(2024, 04, 14, 05, 30, 00, DateTimeKind.Utc),
                    ArrivalTime = new DateTime(2024, 04, 14, 07, 00, 00, DateTimeKind.Utc)
                });

            _flightService = _flightServiceMock.Object;
        }

        [Fact]
        public async Task Should_Return_Flight_When_It_Exists()
        {
            //Arrange
            var expectedFlight = new Flight
            {
                FlightId = "SAMTAS-202404140530",
                Origin = "SAMARKAND",
                Destination = "TASHKENT",
                Capacity = 200,
                AvailableSeats = 200,
                DepartureTime = new DateTime(2024, 04, 14, 05, 30, 00, DateTimeKind.Utc),
                ArrivalTime = new DateTime(2024, 04, 14, 07, 00, 00, DateTimeKind.Utc)
            };

            //Act
            var actualFlight = await _flightService.GetFlight("SAMTAS-202404140530");

            //Assert
            actualFlight.Should().BeEquivalentTo(expectedFlight);
        }

        [Fact]
        public async Task Should_Return_Null_When_It_Flight_Does_Not_Exist()
        {
            //Act
            var actualFlight = await _flightService.GetFlight("SAMTAS-202404140531");

            //Assert
            actualFlight.Should().BeNull();
        }

        [Fact]
        public async Task Should_Decrease_Available_Seat_When_Booked()
        {
            //Arrange
            string flightId = "SAMTAS-202404140531";
            int initialNumberOfSeats = 150;
            _flightServiceMock.Setup(service => service.GetAvailableSeats(flightId))
                .ReturnsAsync(initialNumberOfSeats);
            var expectedNumberOfSeats = initialNumberOfSeats - 1;

            //Act
            var actualFlight = await _flightService.GetFlight(flightId);
            var seatNumber = await _flightService.BookTheSeat(flightId);

            //Assert
            actualFlight?.AvailableSeats.Should().Be(expectedNumberOfSeats);
        }
    }
}