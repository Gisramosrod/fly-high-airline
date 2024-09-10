using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Planes.Commands.UpdatePlane
{
    public sealed record UpdatePlaneCommand(
        Guid Id,
        string Number,
        string Model,
        string Manufacturer) : IRequest<ErrorOr<Success>>;
}
