using AviationFleetService.Api.Contracts.Seats.Enums;

namespace AviationFleetService.Api.Contracts.Seats
{
    public sealed record RegisterSeatRequest(
        SeatClass SeatClass,
        SeatType SeatType,
        string SeatNumber); 
}
