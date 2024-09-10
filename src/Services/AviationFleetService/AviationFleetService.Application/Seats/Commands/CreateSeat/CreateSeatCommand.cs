using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Enums;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Seats.Commands.CreateSeat
{
    public sealed record CreateSeatCommand(
        Guid PlaneId,
        SeatClass SeatClass,
        SeatType SeatType,
        string SeatNumber) : IRequest<ErrorOr<Seat>>;
}
