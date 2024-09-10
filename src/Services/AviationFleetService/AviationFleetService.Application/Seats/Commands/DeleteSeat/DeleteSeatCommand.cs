using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Seats.Commands.DeleteSeat
{
    public sealed record DeleteSeatCommand(Guid PlaneId, Guid SeatId) : IRequest<ErrorOr<Success>>;
}
