using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.Seats;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.Seats.Commands.UpdateSeat
{
    internal sealed class UpdateSeatCommandHandler(
        ISeatRepository seatRepository,
        IPlaneRepository planeRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint) :
        IRequestHandler<UpdateSeatCommand, ErrorOr<Success>>
    {
        private readonly ISeatRepository _seatRepository = seatRepository;
        private readonly IPlaneRepository _planeRepository = planeRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Success>> Handle(UpdateSeatCommand request, CancellationToken cancellationToken)
        {
            var plane = await _planeRepository.GetByIdAsync(request.PlaneId, cancellationToken);
            if (plane is null)
            {
                return PlaneErrors.NotFound(request.PlaneId);
            }

            var updateSeatResponse = plane.UpdateSeat(request.SeatId, request.SeatClass, request.SeatType, request.SeatNumber);
            if (updateSeatResponse.IsError)
            {
                return updateSeatResponse.Errors;
            }

            var seat = updateSeatResponse.Value;

            _seatRepository.Update(seat);
            _planeRepository.Update(plane);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(
                new SeatUpdatedEvent(
                    seat.Id,
                    plane.Id,
                    seat.SeatClass.ToString(),
                    seat.SeatType.ToString()
                    ), cancellationToken);

            return Result.Success;
        }
    }
}
