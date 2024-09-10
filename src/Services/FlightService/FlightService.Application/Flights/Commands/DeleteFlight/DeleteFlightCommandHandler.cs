using ErrorOr;
using FlightService.Domain.Errors;
using FlightService.Domain.Repositories;
using MediatR;

namespace FlightService.Application.Flights.Commands.DeleteFlight {

    internal sealed class DeleteFlightCommandHandler(
        IFlightRepository flightRepository,
        IFlightDetailRepository flightDetailRepository,
        IUnitOfWork unitOfWork) :
        IRequestHandler<DeleteFlightCommand, ErrorOr<Success>> {

        private readonly IFlightRepository _flightRepository = flightRepository;
        private readonly IFlightDetailRepository _flightDetailRepository = flightDetailRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Success>> Handle(DeleteFlightCommand request, CancellationToken cancellationToken) {
       
            var flight = await _flightRepository.GetByIdAsync(request.Id, cancellationToken);

            if (flight is null) {
                return FlightErrors.NotFound(request.Id);
            }

            _flightDetailRepository.Delete(flight.DepartureDetail);
            _flightDetailRepository.Delete(flight.ArrivalDetail);
            _flightRepository.Delete(flight);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success;
        }
    }
}
