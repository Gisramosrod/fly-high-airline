using ErrorOr;
using FlightService.Domain.Enums;
using MediatR;

namespace FlightService.Application.FlightDetails.Commands.UpdateFlightDetail
{

    public sealed record UpdateFlightDetailCommand(
        Guid FlightId,
        FlightDetailType Type,
        DateTime ScheduleDate,
        DateTime ActualDate,
        Guid AirportId) : IRequest<ErrorOr<Success>>;
}
