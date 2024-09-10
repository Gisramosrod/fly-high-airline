using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Errors;
using AviationFleetService.Domain.Repositories;
using Contracts.Airports;
using ErrorOr;
using MassTransit;
using MediatR;

namespace AviationFleetService.Application.Airports.Commands.CreateAirport
{
    internal sealed class CreateAirportCommandHandler(
        IAirportRepository airportRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint) :
        IRequestHandler<CreateAirportCommand, ErrorOr<Airport>>
    {
        private readonly IAirportRepository _airportRepository = airportRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task<ErrorOr<Airport>> Handle(CreateAirportCommand request, CancellationToken cancellationToken)
        {
            if (!await _airportRepository.IsNameUniqueAsync(request.Name, cancellationToken))
            {
                return AirportErrors.NameNotUnique;
            }

            var airport = new Airport(
                Guid.NewGuid(),
                request.Name,
                request.Country,
                request.City,
                request.IsInternational);

            _airportRepository.Add(airport);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(
                new AirportCreatedEvent(
                    airport.Id,
                    airport.Name,
                    airport.Country,
                    airport.City
                ), cancellationToken);

            return airport;
        }
    }
}
