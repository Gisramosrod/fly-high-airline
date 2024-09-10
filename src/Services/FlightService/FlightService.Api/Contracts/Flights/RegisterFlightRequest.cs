namespace FlightService.Api.Contracts.Flights {

    public sealed record RegisterFlightRequest(
        DateTime DepartureScheduleDate,
        Guid DepatureAirportId,
        string DepartureTerminal,
        string DepartureGate,
        DateTime ArrivalScheduleDate,
        Guid ArrivalAirportId,
        string ArrivalTerminal,
        string ArrivalGate,
        Guid PlaneId);
}
