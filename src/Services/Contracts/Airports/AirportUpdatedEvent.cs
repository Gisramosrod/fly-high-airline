namespace Contracts.Airports
{
    public sealed record AirportUpdatedEvent(Guid Id, string Name, string Country, string City);
}
