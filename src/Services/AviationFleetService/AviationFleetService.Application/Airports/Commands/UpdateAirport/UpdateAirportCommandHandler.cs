using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.Airports;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.Airports.Commands.UpdateAirport
{
    internal sealed class UpdateAirportCommandHandler(
        IAirportRepository airportRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint) :
        IRequestHandler<UpdateAirportCommand, ErrorOr<Success>>
    {
        private readonly IAirportRepository _airportRepository = airportRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Success>> Handle(UpdateAirportCommand request, CancellationToken cancellationToken)
        {
            var airport = await _airportRepository.GetByIdAsync(request.Id, cancellationToken);
            if (airport is null)
            {
                return AirportErrors.NotFound(request.Id);
            }

            if (airport.Name != request.Name &&
                !await _airportRepository.IsNameUniqueAsync(request.Name, cancellationToken))
            {
                return AirportErrors.NameNotUnique;
            }

            airport.Set(
                request.Name,
                request.Country,
                request.City,
                request.IsInternational);

            _airportRepository.Update(airport);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(
                new AirportUpdatedEvent(
                    airport.Id,
                    airport.Name,
                    airport.Country,
                    airport.City
                    ), cancellationToken);


            return Result.Success;
        }
    }
}
