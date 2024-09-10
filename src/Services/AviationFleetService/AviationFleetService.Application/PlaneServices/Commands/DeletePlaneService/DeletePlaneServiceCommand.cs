using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.PlaneServices.Commands.DeletePlaneService
{
    public sealed record DeletePlaneServiceCommand(Guid Id) : IRequest<ErrorOr<Success>>;
}
