using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.Airports;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.Airports.Commands.DeleteAirport
{
    internal sealed class DeleteAirportCommandHandler(
        IAirportRepository airportRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint) :
        IRequestHandler<DeleteAirportCommand, ErrorOr<Success>>
    {
        private readonly IAirportRepository _airportRepository = airportRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Success>> Handle(DeleteAirportCommand request, CancellationToken cancellationToken)
        {
            var airport = await _airportRepository.GetByIdAsync(request.Id, cancellationToken);
            if (airport is null)
            {
                return AirportErrors.NotFound(request.Id);
            }

            _airportRepository.Delete(airport);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(new AirportDeletedEvent(airport.Id), cancellationToken);

            return Result.Success;
        }
    }
}
