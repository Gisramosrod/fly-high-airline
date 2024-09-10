using FlightService.Application.Flights.Commands.CreateFlight;
using FlightService.Domain.Repositories;
using FlightService.Domain.Services;
using FlightService.Domain.Entities;
using NSubstitute;
using FluentAssertions;
using FlightService.Domain.Errors;

namespace FlightService.Application.UnitTests.Flights
{
    public class CreateCommandFlightTests
    {
        private static readonly Plane _plane = (Plane?)Activator.CreateInstance(typeof(Plane), true)!;
        private static readonly Airport _airport = (Airport?)Activator.CreateInstance(typeof(Airport), true)!;

        private static readonly CreateFlightCommand _command = new(
            new DateTime(),
            Guid.NewGuid(),
            "DepartureTerminal",
            "DepartureGate",
            new DateTime(),
             Guid.NewGuid(),
            "ArrivalTerminal",
            "ArrivalGate",
            Guid.NewGuid());        

        private readonly CreateFlightCommandHandler _handler;

        private readonly IFlightRepository _flightRepositoryMock;
        private readonly IPlaneRepository _planeRepositoryMock;
        private readonly IAirportRepository _airportRepositoryMock;
        private readonly IFlightDetailRepository _flightDetailRepositoryMock;
        private readonly IFlightNumberService _flightNumberServiceMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public CreateCommandFlightTests()
        {
            _flightRepositoryMock = Substitute.For<IFlightRepository>();
            _planeRepositoryMock = Substitute.For<IPlaneRepository>();
            _airportRepositoryMock = Substitute.For<IAirportRepository>();
            _flightDetailRepositoryMock = Substitute.For<IFlightDetailRepository>();
            _flightNumberServiceMock = Substitute.For<IFlightNumberService>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();

            _handler = new CreateFlightCommandHandler(
                _flightRepositoryMock,
                _planeRepositoryMock,
                _airportRepositoryMock,
                _flightDetailRepositoryMock,
                _flightNumberServiceMock,
                _unitOfWorkMock);
        }

        [Fact]
        public async Task Handler_Should_ReturnError_WhenPlaneIsNull()
        {
            //Arrange
            _planeRepositoryMock.GetByIdAsync(_command.PlaneId, default).Returns((Plane?)null);

            //Act
            var result = await _handler.Handle(_command, default);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Should().Be(PlaneErrors.NotFound(_command.PlaneId));
        }

        [Fact]
        public async Task Handler_Should_ReturnError_WhenDepartureAirportIsNull()
        {
            //Arrange
            _planeRepositoryMock.GetByIdAsync(_command.PlaneId, default).Returns(_plane); 
            _airportRepositoryMock.GetByIdAsync(_command.DepatureAirportId, default).Returns((Airport?)null);

            //Act
            var result = await _handler.Handle(_command, default);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Should().Be(AirportErrors.NotFound(_command.DepatureAirportId));
        }

        [Fact]
        public async Task Handler_Should_ReturnError_WhenArrivalAirportIsNull()
        {
            //Arrange
            _planeRepositoryMock.GetByIdAsync(_command.PlaneId, default).Returns(_plane);
            _airportRepositoryMock.GetByIdAsync(_command.DepatureAirportId, default).Returns(_airport);

            //Act
            var result = await _handler.Handle(_command, default);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Should().Be(AirportErrors.NotFound(_command.ArrivalAirportId));
        }

        [Fact]
        public async Task Handler_Should_ReturnError_WhenFlightNumberIsNotGenerated()
        {
            //Arrange
            _planeRepositoryMock.GetByIdAsync(_command.PlaneId, default).Returns(_plane);
            _airportRepositoryMock.GetByIdAsync(_command.DepatureAirportId, default).Returns(_airport);
            _airportRepositoryMock.GetByIdAsync(_command.ArrivalAirportId, default).Returns(_airport);

            //Act
            var result = await _handler.Handle(_command, default);

            //Assert
            result.IsError.Should().BeTrue();
        }
    }
}
