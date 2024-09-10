using AviationFleetService.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Airports.Queries.GetAirportById
{
    public sealed record GetAirportByIdQuery(Guid Id) : IRequest<ErrorOr<Airport>>;
}
