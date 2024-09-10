using ErrorOr;
using FlightService.Domain.Entities;
using MediatR;

namespace FlightService.Application.Flights.Queries.GetFlightById {

    public sealed record GetFlightByIdQuery(Guid Id) : IRequest<ErrorOr<Flight>>;
}
