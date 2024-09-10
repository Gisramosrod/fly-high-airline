using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace AviationFleetService.Application.Seats.Queries.GetSeatById
{
    internal sealed class GetSeatByIdQueryHandler(
        IPlaneRepository planeRepository) :
        IRequestHandler<GetSeatByIdQuery, ErrorOr<Seat>>
    {
        private readonly IPlaneRepository _planeRepository = planeRepository;

        public async Task<ErrorOr<Seat>> Handle(GetSeatByIdQuery request, CancellationToken cancellationToken)
        {
            var plane = await _planeRepository.GetByIdAsync(request.PlaneId, cancellationToken);
            if (plane is null)
            {
                return PlaneErrors.NotFound(request.PlaneId);
            }

            var seat = plane.Seats.Where(x => x.Id == request.SeatId).FirstOrDefault();
            if (seat is null)
            {
                return SeatErrors.NotFound(request.PlaneId, request.SeatId);
            }

            return seat;
        }
    }
}
