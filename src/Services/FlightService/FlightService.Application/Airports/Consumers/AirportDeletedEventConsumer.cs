using Contracts.Airports;
using FlightService.Domain.Repositories;
using MassTransit;

namespace FlightService.Application.Airports.Consumers
{
    public sealed class AirportDeletedEventConsumer(
        IAirportRepository airportRepository,
        IUnitOfWork unitOfWork)
        : IConsumer<AirportDeletedEvent>
    {
        private readonly IAirportRepository _airportRepository = airportRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Consume(ConsumeContext<AirportDeletedEvent> context)
        {
            var airport = await _airportRepository.GetByIdAsync(context.Message.Id, default);
            if (airport is not null)
            {
                _airportRepository.Delete(airport);
                await _unitOfWork.SaveChangesAsync();
            }           
        }
    }
}
