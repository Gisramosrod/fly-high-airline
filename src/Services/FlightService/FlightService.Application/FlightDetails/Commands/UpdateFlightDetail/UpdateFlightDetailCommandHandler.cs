using ErrorOr;
using FlightService.Domain.Errors;
using FlightService.Domain.Repositories;
using MediatR;

namespace FlightService.Application.FlightDetails.Commands.UpdateFlightDetail
{

    internal sealed class UpdateFlightDetailCommandHandler(
        IFlightRepository flightRepository,
        IAirportRepository airportRepository,
        IFlightDetailRepository flightDetailRepository,
        IUnitOfWork unitOfWork) :
        IRequestHandler<UpdateFlightDetailCommand, ErrorOr<Success>> {

        private readonly IFlightRepository _flightRepository = flightRepository;
        private readonly IAirportRepository _airportRepository = airportRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Success>> Handle(UpdateFlightDetailCommand request, CancellationToken cancellationToken) {

            var flight = await _flightRepository.GetByIdAsync(request.FlightId, cancellationToken);
            if (flight is null) {
                return FlightErrors.NotFound(request.FlightId);
            }

            var airport = await _airportRepository.GetByIdAsync(request.AirportId, cancellationToken);
            if (airport is null) {
                return AirportErrors.NotFound(request.AirportId);
            }

            flight.SetFlighDetail(
                request.Type, 
                request.ScheduleDate, 
                request.ActualDate, 
                airport);

            var flightDetail = 
                request.Type == Domain.Enums.FlightDetailType.Departure 
                ? flight.DepartureDetail 
                : flight.ArrivalDetail;

            flightDetailRepository.Update(flightDetail);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
