namespace Contracts.Seats
{
    public sealed record SeatDeletedEvent(Guid Id, Guid PlaneId);

}
