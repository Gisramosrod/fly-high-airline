using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.PlaneServices;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.PlaneServices.Commands.DeletePlaneService
{
    internal sealed class DeletePlaneServiceCommandHandler(
        IPlaneServiceRepository planeServiceRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint) :
        IRequestHandler<DeletePlaneServiceCommand, ErrorOr<Success>>
    {
        private readonly IPlaneServiceRepository _planeServiceRepository = planeServiceRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Success>> Handle(DeletePlaneServiceCommand request, CancellationToken cancellationToken)
        {
            var planeService = await _planeServiceRepository.GetByIdAsync(request.Id, cancellationToken);
            if (planeService is null)
            {
                return PlaneServiceErrors.NotFound(request.Id);
            }

            _planeServiceRepository.Delete(planeService);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(new PlaneServiceDeletedEvent(planeService.Id), cancellationToken);

            return Result.Success;
        }
    }
}
