namespace Contracts.Planes
{
    public sealed record class PlaneCreatedEvent(Guid Id, string Number, string Model);
}
