using AviationFleetService.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Seats.Queries.GetSeatById
{
    public sealed record GetSeatByIdQuery(Guid PlaneId, Guid SeatId) : IRequest<ErrorOr<Seat>>;
}
