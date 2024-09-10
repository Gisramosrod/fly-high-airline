namespace AviationFleetService.Api.Contracts.Airports
{
    public record AirportResponse(
        Guid Id,
        string Name,
        string Country,
        string City,
        bool IsInternational);
}
