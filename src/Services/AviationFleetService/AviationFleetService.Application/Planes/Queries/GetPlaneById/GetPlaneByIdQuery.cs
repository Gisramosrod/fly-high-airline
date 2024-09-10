using AviationFleetService.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Planes.Queries.GetPlaneById
{
    public sealed record GetPlaneByIdQuery(Guid Id) : IRequest<ErrorOr<Plane>>;
}
