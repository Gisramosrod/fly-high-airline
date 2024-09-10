using ErrorOr;
using FlightService.Domain.Entities;
using FlightService.Domain.Errors;
using FlightService.Domain.Repositories;
using FlightService.Domain.Services;
using MediatR;

namespace FlightService.Application.Flights.Commands.CreateFlight
{
    internal sealed class CreateFlightCommandHandler(
        IFlightRepository flightRepository,
        IPlaneRepository planeRepository,
        IAirportRepository airportRepository,
        IFlightDetailRepository flightDetailRepository,
        IFlightNumberService flightNumberService,
        IUnitOfWork unitOfWork) :
        IRequestHandler<CreateFlightCommand, ErrorOr<Flight>> {

        private readonly IFlightRepository _flightRepository = flightRepository;
        private readonly IPlaneRepository _planeRepository = planeRepository;
        private readonly IAirportRepository _airportRepository = airportRepository;
        private readonly IFlightDetailRepository _flightDetailRepository = flightDetailRepository;
        private readonly IFlightNumberService _flightNumberService = flightNumberService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Flight>> Handle(CreateFlightCommand request, CancellationToken cancellationToken) {

            var plane = await _planeRepository.GetByIdAsync(request.PlaneId, cancellationToken);
            if (plane is null) {
                return PlaneErrors.NotFound(request.PlaneId);
            }

            var departureAirport = await _airportRepository.GetByIdAsync(request.DepatureAirportId, cancellationToken);
            if (departureAirport is null) {
                return AirportErrors.NotFound(request.DepatureAirportId);
            }

            var arrivalAirport = await _airportRepository.GetByIdAsync(request.ArrivalAirportId, cancellationToken);
            if (arrivalAirport is null) {
                return AirportErrors.NotFound(request.ArrivalAirportId);
            }

            var flightNumberResult = await _flightNumberService.GenerateFlightNumber(cancellationToken);
            if (flightNumberResult.IsError) {
                return flightNumberResult.Errors;
            }

            var flightNumber = flightNumberResult.Value;

            var flightResult = Flight.Create(
                Guid.NewGuid(),
                flightNumber,
                plane,
                request.DepartureScheduleDate,
                departureAirport,
                request.ArrivalScheduleDate,
                arrivalAirport);

            if (flightResult.IsError) {
                return flightResult.Errors;
            }

            var flight = flightResult.Value;

            _flightRepository.Add(flight);
            _flightDetailRepository.Add(flight.DepartureDetail);
            _flightDetailRepository.Add(flight.ArrivalDetail);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return flight;
        }
    }
}
