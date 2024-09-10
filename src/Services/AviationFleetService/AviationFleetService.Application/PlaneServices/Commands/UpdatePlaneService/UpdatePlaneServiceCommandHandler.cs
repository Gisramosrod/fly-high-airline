using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.PlaneServices;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.PlaneServices.Commands.UpdatePlaneService
{
    internal sealed class UpdatePlaneServiceCommandHandler(
        IPlaneServiceRepository planeServiceRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint) :
        IRequestHandler<UpdatePlaneServiceCommand, ErrorOr<Success>>
    {
        private readonly IPlaneServiceRepository _planeServiceRepository = planeServiceRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Success>> Handle(UpdatePlaneServiceCommand request, CancellationToken cancellationToken)
        {
            var planeService = await _planeServiceRepository.GetByIdAsync(request.Id, cancellationToken);
            if (planeService is null)
            {
                return PlaneServiceErrors.NotFound(request.Id);
            }

            if (planeService.Name != request.Name &&
                !await _planeServiceRepository.IsNameUniqueAsync(request.Name, cancellationToken))
            {
                return PlaneServiceErrors.NameNotUnique;
            }

            if (planeService.Description != request.Description &&
              !await _planeServiceRepository.IsDescriptionUniqueAsync(request.Description, cancellationToken))
            {
                return PlaneServiceErrors.DescriptionNotUnique;
            }

            planeService.Set(
                    request.Name,
                    request.Description);

            _planeServiceRepository.Update(planeService);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(
                new PlaneServiceUpdatedEvent(
                    planeService.Id,
                    planeService.Name,
                    planeService.Description
                    ), cancellationToken);

            return Result.Success;
        }
    }
}
