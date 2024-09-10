using ErrorOr;
using FlightService.Domain.Entities;
using FlightService.Domain.Errors;
using FlightService.Domain.Repositories;
using MediatR;

namespace FlightService.Application.Flights.Queries.GetFlightById {

    internal sealed class GetFlightByIdQueryHandler(IFlightRepository flightRepository) : IRequestHandler<GetFlightByIdQuery, ErrorOr<Flight>> {

        private readonly IFlightRepository _flightRepository = flightRepository;

        public async Task<ErrorOr<Flight>> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken) {

            var flight = await _flightRepository.GetByIdAsync(request.Id, cancellationToken);

            if (flight is null) {
                return FlightErrors.NotFound(request.Id);
            }

            return flight;
        }
    }
}
