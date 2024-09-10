namespace AviationFleetService.Api.Contracts.Airports
{
    public record RegisterAirportRequest(
        string Name,
        string Country,
        string City,
        bool IsInternational);
}
