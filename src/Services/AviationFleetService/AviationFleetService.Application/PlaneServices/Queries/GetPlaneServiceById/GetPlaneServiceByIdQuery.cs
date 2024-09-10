using AviationFleetService.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.PlaneServices.Queries.GetPlaneServiceById
{
    public sealed record GetPlaneServiceByIdQuery(Guid Id) : IRequest<ErrorOr<PlaneService>>;
}
