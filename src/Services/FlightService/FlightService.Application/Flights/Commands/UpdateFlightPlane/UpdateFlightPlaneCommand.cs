using ErrorOr;
using MediatR;

namespace FlightService.Application.Flights.Commands.UpdateFlightPlane {

    public sealed record UpdateFlightPlaneCommand(Guid FlightId, Guid PlaneId) : IRequest<ErrorOr<Success>>;
}
