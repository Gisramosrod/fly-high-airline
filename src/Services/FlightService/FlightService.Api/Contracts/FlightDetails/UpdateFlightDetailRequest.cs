namespace FlightService.Api.Contracts.FlightDetails
{

    public sealed record UpdateFlightDetailRequest(
        DateTime ScheduleDate,
        DateTime ActualDate,
        Guid AirportId);

}
