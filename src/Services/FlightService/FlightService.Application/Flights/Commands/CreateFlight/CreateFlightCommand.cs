using ErrorOr;
using FlightService.Domain.Entities;
using MediatR;

namespace FlightService.Application.Flights.Commands.CreateFlight {

    public sealed record CreateFlightCommand(
        DateTime DepartureScheduleDate,
        Guid DepatureAirportId,
        string DepartureTerminal,
        string DepartureGate,
        DateTime ArrivalScheduleDate,
        Guid ArrivalAirportId,
        string ArrivalTerminal,
        string ArrivalGate,
        Guid PlaneId) : IRequest<ErrorOr<Flight>>;
}
