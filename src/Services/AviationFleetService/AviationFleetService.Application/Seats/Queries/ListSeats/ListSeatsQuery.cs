using AviationFleetService.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Seats.Queries.ListSeats
{
    public sealed record ListSeatsQuery(Guid PlaneId) : IRequest<ErrorOr<List<Seat>>>;
}
