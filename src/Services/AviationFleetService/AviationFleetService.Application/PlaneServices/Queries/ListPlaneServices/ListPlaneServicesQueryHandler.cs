using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.PlaneServices.Queries.ListPlaneServices
{
    internal sealed class ListPlaneServicesQueryHandler(IPlaneServiceRepository planeServiceRepository) :
        IRequestHandler<ListPlaneServicesQuery, ErrorOr<List<PlaneService>>>
    {
        private readonly IPlaneServiceRepository _planeServiceRepository = planeServiceRepository;

        public async Task<ErrorOr<List<PlaneService>>> Handle(ListPlaneServicesQuery request, CancellationToken cancellationToken)
        {
            return await _planeServiceRepository.ListAsync(cancellationToken);
        }
    }
}
