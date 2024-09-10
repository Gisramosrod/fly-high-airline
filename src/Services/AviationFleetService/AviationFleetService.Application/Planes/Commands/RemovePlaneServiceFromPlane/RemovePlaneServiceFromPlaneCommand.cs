using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Planes.Commands.RemovePlaneServiceFromPlane
{
    public sealed record RemovePlaneServiceFromPlaneCommand(Guid PlaneId, Guid PlaneServiceId) : IRequest<ErrorOr<Success>>;
}
