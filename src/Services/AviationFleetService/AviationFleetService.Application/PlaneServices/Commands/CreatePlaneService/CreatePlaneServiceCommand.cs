using AviationFleetService.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.PlaneServices.Commands.CreatePlaneService
{
    public sealed record CreatePlaneServiceCommand(string Name, string Description) : IRequest<ErrorOr<PlaneService>>;
}
