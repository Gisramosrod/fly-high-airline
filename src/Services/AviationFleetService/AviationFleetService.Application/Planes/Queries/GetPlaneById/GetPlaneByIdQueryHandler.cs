using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Planes.Queries.GetPlaneById
{
    internal sealed class GetPlaneByIdQueryHandler(IPlaneRepository planeRepository) :
        IRequestHandler<GetPlaneByIdQuery, ErrorOr<Plane>>
    {
        private readonly IPlaneRepository _planeRepository = planeRepository;

        public async Task<ErrorOr<Plane>> Handle(GetPlaneByIdQuery request, CancellationToken cancellationToken)
        {
            var plane = await _planeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (plane is null)
            {
                return PlaneErrors.NotFound(request.Id);
            }

            return plane;
        }
    }
}
