using FlightService.Domain.Entities;
using FlightService.Domain.Repositories;
using FlightService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace FlightService.Infrastructure.Repositories
{
    internal sealed class AirportRepository(AppDbContext dbContext) : IAirportRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public void Add(Airport airport)
        {
            _dbContext.Airports.Add(airport);
        }

        public void Delete(Airport airport)
        {
            _dbContext.Airports.Remove(airport);
        }

        public async Task<Airport?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Airports.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public void Update(Airport airport)
        {
            _dbContext.Airports.Update(airport);
        }
    }
}
