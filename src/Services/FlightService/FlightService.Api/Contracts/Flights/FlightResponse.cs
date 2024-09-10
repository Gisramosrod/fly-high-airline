using FlightService.Api.Contracts.FlightsStatus;

namespace FlightService.Api.Contracts.Flights {

    public sealed record FlightResponse(
        Guid Id,
        string Number,
        FlightStatus Status,
        TimeSpan Duration,
        Guid PlaneId,
        Guid DepartureDetailId,
        Guid ArrivalDetailId);
}
