using Contracts.Airports;
using FlightService.Domain.Repositories;
using MassTransit;

namespace FlightService.Application.Airports.Consumers
{
    public sealed class AirportUpdatedEventConsumer(
        IAirportRepository airportRepository,
        IUnitOfWork unitOfWork)
        : IConsumer<AirportUpdatedEvent>
    {
        private readonly IAirportRepository _airportRepository = airportRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Consume(ConsumeContext<AirportUpdatedEvent> context)
        {
            var airport = await _airportRepository.GetByIdAsync(context.Message.Id, default);
            if (airport is not null)
            {
                airport.Set(context.Message.Name, context.Message.Country, context.Message.City);
                _airportRepository.Update(airport);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
