using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Planes.Queries.ListPlanes
{
    internal sealed class ListPlanesQueryHandler(IPlaneRepository planeRepository) :
        IRequestHandler<ListPlanesQuery, ErrorOr<List<Plane>>>
    {
        private readonly IPlaneRepository _planeRepository = planeRepository;

        public async Task<ErrorOr<List<Plane>>> Handle(ListPlanesQuery request, CancellationToken cancellationToken)
        {
            return await _planeRepository.ListAsync(cancellationToken);
        }
    }
}
