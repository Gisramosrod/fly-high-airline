using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.Planes;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.Planes.Commands.RemovePlaneServiceFromPlane
{
    internal sealed class RemovePlaneServiceFromPlaneCommandHandler(
        IPlaneRepository planeRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint
        ) : IRequestHandler<RemovePlaneServiceFromPlaneCommand, ErrorOr<Success>>
    {
        private readonly IPlaneRepository _planeRepository = planeRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Success>> Handle(RemovePlaneServiceFromPlaneCommand request, CancellationToken cancellationToken)
        {
            var plane = await _planeRepository.GetByIdAsync(request.PlaneId, cancellationToken);
            if (plane is null)
            {
                return PlaneErrors.NotFound(request.PlaneId);
            }

            var removePlaneServiceResult = plane.RemovePlaneService(request.PlaneServiceId);
            if (removePlaneServiceResult.IsError)
            {
                return removePlaneServiceResult.Errors;
            }

            _planeRepository.Update(plane);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(
                new PlaneServiceRemoveFromPlaneEvent(
                    plane.Id,
                    request.PlaneServiceId
                    ), cancellationToken);

            return Result.Success;
        }
    }
}
