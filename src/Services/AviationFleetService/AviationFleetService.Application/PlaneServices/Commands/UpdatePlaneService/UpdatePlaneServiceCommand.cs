using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.PlaneServices.Commands.UpdatePlaneService
{
    public sealed record UpdatePlaneServiceCommand(Guid Id, string Name, string Description) : IRequest<ErrorOr<Success>>;
}
