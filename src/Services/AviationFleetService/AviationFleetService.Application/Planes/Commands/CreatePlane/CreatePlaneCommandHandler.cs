using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.Planes;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.Planes.Commands.CreatePlane
{
    internal sealed class CreatePlaneCommandHandler(
        IPlaneRepository planeRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint) :
        IRequestHandler<CreatePlaneCommand, ErrorOr<Plane>>
    {
        private readonly IPlaneRepository _planeRepository = planeRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Plane>> Handle(CreatePlaneCommand request, CancellationToken cancellationToken)
        {
            if (!await _planeRepository.IsNumberUniqueAsync(request.Number, cancellationToken))
            {
                return PlaneErrors.NumberNotUnique;
            }

            var plane = new Plane(
                Guid.NewGuid(),
                request.Number,
                request.Model,
                request.Manufacturer);

            _planeRepository.Add(plane);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(
                new PlaneCreatedEvent(
                    plane.Id,
                    plane.Number,
                    plane.Model
                    ), cancellationToken);

            return plane;
        }
    }
}
