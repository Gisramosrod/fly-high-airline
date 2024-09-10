using ErrorOr;
using FlightService.Domain.Entities;
using FlightService.Domain.Repositories;
using MediatR;

namespace FlightService.Application.Flights.Queries.ListFlights
{
    internal sealed class ListFlightsQueryHandler(IFlightRepository flightRepository) :
        IRequestHandler<ListFlightsQuery, ErrorOr<List<Flight>>>
    {
        private readonly IFlightRepository _flightRepository = flightRepository;

        public async Task<ErrorOr<List<Flight>>> Handle(ListFlightsQuery request, CancellationToken cancellationToken)
        {
            return await _flightRepository.ListAsync(cancellationToken);
        }
    }
}
