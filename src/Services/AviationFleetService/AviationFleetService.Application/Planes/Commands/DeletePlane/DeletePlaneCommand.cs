using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Planes.Commands.DeletePlane
{
    public sealed record DeletePlaneCommand(Guid Id) : IRequest<ErrorOr<Success>>;
}
