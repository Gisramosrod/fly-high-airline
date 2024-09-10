using ErrorOr;
using MediatR;

namespace FlightService.Application.Flights.Commands.DeleteFlight {

    public sealed record DeleteFlightCommand(Guid Id) : IRequest<ErrorOr<Success>>;
}
