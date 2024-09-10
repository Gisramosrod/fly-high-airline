using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.Seats;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.Seats.Commands.CreateSeat
{
    internal sealed class CreateSeatCommandHandler(
        ISeatRepository seatRepository,
        IPlaneRepository planeRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint) :
        IRequestHandler<CreateSeatCommand, ErrorOr<Seat>>
    {
        private readonly ISeatRepository _seatRepository = seatRepository;
        private readonly IPlaneRepository _planeRepository = planeRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Seat>> Handle(CreateSeatCommand request, CancellationToken cancellationToken)
        {
            var plane = await _planeRepository.GetByIdAsync(request.PlaneId, cancellationToken);
            if (plane is null)
            {
                return PlaneErrors.NotFound(request.PlaneId);
            }

            var addSeatResponse = plane.AddSeat(request.SeatClass, request.SeatType, request.SeatNumber);
            if (addSeatResponse.IsError)
            {
                return addSeatResponse.Errors;
            }

            var seat = addSeatResponse.Value;

            _seatRepository.Add(seat); 
            _planeRepository.Update(plane);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(
                new SeatsCreatedEvent(
                    seat.Id,
                    plane.Id,
                    seat.SeatClass.ToString(),
                    seat.SeatType.ToString()
                    ), cancellationToken);

            return seat;
        }
    }
}
