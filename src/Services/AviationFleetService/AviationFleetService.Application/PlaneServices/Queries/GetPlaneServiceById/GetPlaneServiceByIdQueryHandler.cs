using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.PlaneServices.Queries.GetPlaneServiceById
{
    internal sealed class GetPlaneServiceByIdQueryHandler(IPlaneServiceRepository planeServiceRepository) :
        IRequestHandler<GetPlaneServiceByIdQuery, ErrorOr<PlaneService>>
    {
        private readonly IPlaneServiceRepository _planeServiceRepository = planeServiceRepository;

        public async Task<ErrorOr<PlaneService>> Handle(GetPlaneServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var planeService = await _planeServiceRepository.GetByIdAsync(request.Id, cancellationToken);
            if (planeService is null)
            {
                return PlaneServiceErrors.NotFound(request.Id);
            }

            return planeService;
        }
    }
}
