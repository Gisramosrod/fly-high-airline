using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Airports.Queries.GetAirportById
{
    internal sealed class GetAirportByIdQueryHandler(IAirportRepository airportRepository) :
        IRequestHandler<GetAirportByIdQuery, ErrorOr<Airport>>
    {
        private readonly IAirportRepository _airportRepository = airportRepository;

        public async Task<ErrorOr<Airport>> Handle(GetAirportByIdQuery request, CancellationToken cancellationToken)
        {
            var airport = await _airportRepository.GetByIdAsync(request.Id, cancellationToken);
            if (airport is null)
            {
                return AirportErrors.NotFound(request.Id);
            }

            return airport;
        }
    }
}
