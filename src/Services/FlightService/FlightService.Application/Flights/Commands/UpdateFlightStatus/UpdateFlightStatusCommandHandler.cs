using ErrorOr;
using FlightService.Domain.Errors;
using FlightService.Domain.Repositories;
using MediatR;

namespace FlightService.Application.Flights.Commands.UpdateFlightStatus {

    internal sealed class UpdateFlightStatusCommandHandler(
        IFlightRepository flightRepository,
        IUnitOfWork unitOfWork) :
        IRequestHandler<UpdateFlightStatusCommand, ErrorOr<Success>> {

        private readonly IFlightRepository _flightRepository = flightRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ErrorOr<Success>> Handle(UpdateFlightStatusCommand request, CancellationToken cancellationToken) {

            var flight = await _flightRepository.GetByIdAsync(request.Id, cancellationToken);
            if(flight is null) {
                return FlightErrors.NotFound(request.Id);
            }

            flight.SetStatus(request.Status);

            _flightRepository.Update(flight);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
