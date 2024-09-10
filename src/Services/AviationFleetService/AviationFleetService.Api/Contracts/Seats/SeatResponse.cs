using AviationFleetService.Api.Contracts.Seats.Enums;

namespace AviationFleetService.Api.Contracts.Seats
{
    public sealed record SeatResponse(Guid Id, string SeatNumber, SeatClass SeatClass, SeatType SeatType);
}
