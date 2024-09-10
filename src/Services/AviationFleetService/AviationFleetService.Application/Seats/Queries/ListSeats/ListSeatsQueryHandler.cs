using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Seats.Queries.ListSeats
{
    internal sealed class ListSeatsQueryHandler(IPlaneRepository planeRepository) :
        IRequestHandler<ListSeatsQuery, ErrorOr<List<Seat>>>
    {
        private readonly IPlaneRepository _planeRepository = planeRepository;

        public async Task<ErrorOr<List<Seat>>> Handle(ListSeatsQuery request, CancellationToken cancellationToken)
        {
            var plane = await _planeRepository.GetByIdAsync(request.PlaneId, cancellationToken);
            if (plane is null)
            {
                return PlaneErrors.NotFound(request.PlaneId);
            }

            return plane.Seats.ToList();
        }
    }
}
