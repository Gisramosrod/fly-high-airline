namespace Contracts.PlaneServices
{
    public sealed record PlaneServiceCreatedEvent(Guid Id, string Name, string Description);
}
