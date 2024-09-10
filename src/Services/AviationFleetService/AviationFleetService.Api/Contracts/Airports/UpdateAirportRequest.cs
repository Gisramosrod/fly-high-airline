namespace AviationFleetService.Api.Contracts.Airports
{
    public record UpdateAirportRequest(
        string Name,
        string Country,
        string City,
        bool IsInternational);
}
