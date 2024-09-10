using AviationFleetService.Domain.Enums;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Seats.Commands.UpdateSeat
{
    public sealed record UpdateSeatCommand(
        Guid PlaneId,
        Guid SeatId,
        SeatClass SeatClass,
        SeatType SeatType,
        string SeatNumber) : IRequest<ErrorOr<Success>>;
}
