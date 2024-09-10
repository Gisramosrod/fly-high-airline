using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.Seats;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.Seats.Commands.DeleteSeat
{
    internal sealed class DeleteSeatCommandHandler(
        ISeatRepository seatRepository,
        IPlaneRepository planeRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint) :
        IRequestHandler<DeleteSeatCommand, ErrorOr<Success>>
    {
        private readonly ISeatRepository _seatRepository = seatRepository;
        private readonly IPlaneRepository _planeRepository = planeRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Success>> Handle(DeleteSeatCommand request, CancellationToken cancellationToken)
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

            plane.RemoveSeat(seat);

            _planeRepository.Update(plane);
            _seatRepository.Delete(seat);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(
                new SeatDeletedEvent(
                    seat.Id,
                    plane.Id
                    ), cancellationToken);

            return Result.Success;
        }
    }
}
