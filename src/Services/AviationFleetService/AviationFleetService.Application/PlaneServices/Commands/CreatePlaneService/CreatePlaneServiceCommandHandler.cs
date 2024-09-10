using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.Airports;
using Contracts.PlaneServices;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.PlaneServices.Commands.CreatePlaneService
{
    internal sealed class CreatePlaneServiceCommandHandler(
        IPlaneServiceRepository planeServiceRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint) :
        IRequestHandler<CreatePlaneServiceCommand, ErrorOr<PlaneService>>
    {
        private readonly IPlaneServiceRepository _planeServiceRepository = planeServiceRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<PlaneService>> Handle(CreatePlaneServiceCommand request, CancellationToken cancellationToken)
        {
            if (!await _planeServiceRepository.IsNameUniqueAsync(request.Name, cancellationToken))
            {
                return PlaneServiceErrors.NameNotUnique;
            }

            if (!await _planeServiceRepository.IsDescriptionUniqueAsync(request.Description, cancellationToken))
            {
                return PlaneServiceErrors.DescriptionNotUnique;
            }

            var planeService = new PlaneService(
                Guid.NewGuid(),
                request.Name,
                request.Description);

            _planeServiceRepository.Add(planeService);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(
                new PlaneServiceCreatedEvent(
                    planeService.Id,
                    planeService.Name,
                    planeService.Description
                    ), cancellationToken);

            return planeService;
        }
    }
}
