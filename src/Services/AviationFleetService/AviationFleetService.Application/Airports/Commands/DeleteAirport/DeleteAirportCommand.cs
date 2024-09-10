using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Airports.Commands.DeleteAirport
{
    public sealed record DeleteAirportCommand(Guid Id) : IRequest<ErrorOr<Success>>;
}
