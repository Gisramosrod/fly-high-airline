using ErrorOr;
using FlightService.Domain.Enums;
using MediatR;

namespace FlightService.Application.Flights.Commands.UpdateFlightStatus {

    public sealed record UpdateFlightStatusCommand(Guid Id, FlightStatus Status) : IRequest<ErrorOr<Success>>;
}
