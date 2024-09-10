using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Airports.Queries.ListAirports
{
    internal sealed class ListAirportQueryHandler(IAirportRepository airportRepository) :
        IRequestHandler<ListAirportsQuery, ErrorOr<List<Airport>>>
    {
        private readonly IAirportRepository _airportRepository = airportRepository;

        public async Task<ErrorOr<List<Airport>>> Handle(ListAirportsQuery request, CancellationToken cancellationToken)
        {
            return await _airportRepository.ListAsync(cancellationToken);
        }
    }
}
