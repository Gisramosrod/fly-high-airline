using AviationFleetService.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Airports.Commands.CreateAirport
{
    public sealed record CreateAirportCommand(
        string Name,
        string Country,
        string City,
        bool IsInternational) : IRequest<ErrorOr<Airport>>;
}
