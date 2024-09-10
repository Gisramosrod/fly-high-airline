namespace Contracts.Planes
{
    public sealed record PlaneUpdatedEvent(Guid Id, string Number, string Model);
}
