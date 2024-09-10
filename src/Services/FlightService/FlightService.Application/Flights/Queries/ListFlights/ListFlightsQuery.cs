using ErrorOr;
using FlightService.Domain.Entities;
using MediatR;

namespace FlightService.Application.Flights.Queries.ListFlights {

    public sealed record ListFlightsQuery : IRequest<ErrorOr<List<Flight>>>;
}
