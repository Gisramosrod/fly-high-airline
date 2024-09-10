namespace Contracts.PlaneServices
{
    public sealed record PlaneServiceUpdatedEvent(Guid Id, string Name, string Description);
}
