namespace Contracts.Airports
{
    public sealed record AirportCreatedEvent(Guid Id, string Name, string Country, string City);
}
