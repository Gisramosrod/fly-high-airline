using FlightService.Domain.Entities;
using FlightService.Domain.Repositories;
using FlightService.Infrastructure.Common;

namespace FlightService.Infrastructure.Repositories
{
    internal sealed class FlightDetailRepository(AppDbContext dbContext) : IFlightDetailRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public void Add(FlightDetail flightDetail)
        {
            _dbContext.FlightDetails.Add(flightDetail);
        }

        public void Delete(FlightDetail flightDetail)
        {
            _dbContext.FlightDetails.Remove(flightDetail);
        }

        public void Update(FlightDetail flightDetail)
        {
            _dbContext.FlightDetails.Update(flightDetail);
        }
    }
}
