using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.Planes;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.Planes.Commands.DeletePlane
{
    internal sealed class DeletePlaneCommandHandler(
        IPlaneRepository planeRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint) :
        IRequestHandler<DeletePlaneCommand, ErrorOr<Success>>
    {
        private readonly IPlaneRepository _planeRepository = planeRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Success>> Handle(DeletePlaneCommand request, CancellationToken cancellationToken)
        {
            var plane = await _planeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (plane is null)
            {
                return PlaneErrors.NotFound(request.Id);
            }

            _planeRepository.Delete(plane);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(new PlaneDeletedEvent(plane.Id), cancellationToken);

            return Result.Success;
        }
    }
}
