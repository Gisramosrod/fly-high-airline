namespace Contracts.Seats
{
    public sealed record SeatUpdatedEvent(Guid Id, Guid PlaneId, string SeatClass, string SeatType);
}
