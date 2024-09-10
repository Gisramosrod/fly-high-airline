using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.Planes;
using Contracts.Seats;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.Planes.Commands.AddPlaneServiceToPlane
{
    internal sealed class AddPlaneServiceToPlaneCommandHandler(
        IPlaneRepository planeRepository,
        IPlaneServiceRepository planeServiceRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint
        ) : IRequestHandler<AddPlaneServiceToPlaneCommand, ErrorOr<Success>>
    {
        private readonly IPlaneRepository _planeRepository = planeRepository;
        private readonly IPlaneServiceRepository _planeServiceRepository = planeServiceRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Success>> Handle(AddPlaneServiceToPlaneCommand request, CancellationToken cancellationToken)
        {
            var plane = await _planeRepository.GetByIdAsync(request.PlaneId, cancellationToken);
            if (plane is null)
            {
                return PlaneErrors.NotFound(request.PlaneId);
            }

            var planeService = await _planeServiceRepository.GetByIdAsync(request.PlaneServiceId, cancellationToken);
            if (planeService is null)
            {
                return PlaneServiceErrors.NotFound(request.PlaneServiceId);
            }

            var addPlaneResult = plane.AddPlaneService(planeService);
            if (addPlaneResult.IsError)
            {
                return addPlaneResult.Errors;
            }

            _planeRepository.Update(plane);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(                
                new PlaneServiceAddedToPlaneEvent(
                    plane.Id, 
                    planeService.Id              
                    ), cancellationToken);

            return Result.Success;
        }
    }
}
