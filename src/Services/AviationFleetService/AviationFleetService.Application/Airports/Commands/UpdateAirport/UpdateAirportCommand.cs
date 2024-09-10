using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Airports.Commands.UpdateAirport
{
    public sealed record UpdateAirportCommand(
        Guid Id,
        string Name,
        string Country,
        string City,
        bool IsInternational) : IRequest<ErrorOr<Success>>;
}
