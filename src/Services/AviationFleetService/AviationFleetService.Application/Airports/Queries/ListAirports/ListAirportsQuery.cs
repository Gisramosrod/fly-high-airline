using AviationFleetService.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Airports.Queries.ListAirports
{
    public record ListAirportsQuery : IRequest<ErrorOr<List<Airport>>>;
}
