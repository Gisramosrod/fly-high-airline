using AviationFleetService.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Planes.Queries.ListPlanes
{
    public sealed record ListPlanesQuery : IRequest<ErrorOr<List<Plane>>>;

}
