namespace Contracts.Seats
{
    public sealed record SeatsCreatedEvent(Guid Id, Guid PlaneId, string SeatClass, string SeatType);
}
