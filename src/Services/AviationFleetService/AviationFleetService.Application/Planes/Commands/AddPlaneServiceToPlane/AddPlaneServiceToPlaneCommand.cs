using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Planes.Commands.AddPlaneServiceToPlane
{
    public sealed record AddPlaneServiceToPlaneCommand(Guid PlaneId, Guid PlaneServiceId) : IRequest<ErrorOr<Success>>;
}
