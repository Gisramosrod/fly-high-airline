using ErrorOr;
using FlightService.Domain.Errors;
using FlightService.Domain.Repositories;
using MediatR;

namespace FlightService.Application.Flights.Commands.UpdateFlightPlane {

    internal sealed class UpdateFlightPlaneCommandHandler(
        IFlightRepository flightRepository,
        IPlaneRepository planeRepository,
        IUnitOfWork unitOfWork) :
        IRequestHandler<UpdateFlightPlaneCommand, ErrorOr<Success>> {

        private readonly IFlightRepository _flightRepository = flightRepository;
        private readonly IPlaneRepository _planeRepository = planeRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Success>> Handle(UpdateFlightPlaneCommand request, CancellationToken cancellationToken) {

            var plane = await _planeRepository.GetByIdAsync(request.PlaneId, cancellationToken);
            if (plane is null) {
                return PlaneErrors.NotFound(request.PlaneId);
            }

            var flight = await _flightRepository.GetByIdAsync(request.FlightId, cancellationToken);
            if (flight is null) {
                return FlightErrors.NotFound(request.FlightId);
            }

            var setPlaneResult = flight.SetPlane(plane);
            if (setPlaneResult.IsError) {
                return setPlaneResult.Errors;
            }

            _flightRepository.Update(flight);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
