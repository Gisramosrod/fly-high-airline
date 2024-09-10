using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.Planes;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.Planes.Commands.UpdatePlane
{
    internal sealed class UpdatePlaneCommandHandler(
        IPlaneRepository planeRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint) :
        IRequestHandler<UpdatePlaneCommand, ErrorOr<Success>>
    {
        private readonly IPlaneRepository _planeRepository = planeRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Success>> Handle(UpdatePlaneCommand request, CancellationToken cancellationToken)
        {
            var plane = await _planeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (plane is null)
            {
                return PlaneErrors.NotFound(request.Id);
            }

            if (plane.Number != request.Number && !await _planeRepository.IsNumberUniqueAsync(request.Number, cancellationToken))
            {
                return PlaneErrors.NumberNotUnique;
            }

            plane.Set(
                request.Number,
                request.Model,
                request.Manufacturer);

            _planeRepository.Update(plane);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(
                new PlaneUpdatedEvent(
                    plane.Id,
                    plane.Number,
                    plane.Model
                    ), cancellationToken);

            return Result.Success;
        }
    }
}
