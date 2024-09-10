using AviationFleetService.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Planes.Commands.CreatePlane
{
    public sealed record CreatePlaneCommand(
        string Number, 
        string Model,
        string Manufacturer) : IRequest<ErrorOr<Plane>>;
}
