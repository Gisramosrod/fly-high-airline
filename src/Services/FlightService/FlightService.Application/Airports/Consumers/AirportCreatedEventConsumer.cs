using Contracts.Airports;
using FlightService.Domain.Entities;
using FlightService.Domain.Repositories;
using MassTransit;

namespace FlightService.Application.Airports.Consumers
{
    public sealed class AirportCreatedEventConsumer(
        IAirportRepository airportRepository,
        IUnitOfWork unitOfWork)
        : IConsumer<AirportCreatedEvent>
    {
        private readonly IAirportRepository _airportRepository = airportRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Consume(ConsumeContext<AirportCreatedEvent> context)
        {
            var airport = new Airport(
                context.Message.Id,
                context.Message.Name,
                context.Message.Country,
                context.Message.City);

            _airportRepository.Add(airport);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
