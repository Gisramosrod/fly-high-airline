using AviationFleetService.Domain.Entities;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.PlaneServices.Queries.ListPlaneServices
{
    public sealed record ListPlaneServicesQuery : IRequest<ErrorOr<List<PlaneService>>>;

}
