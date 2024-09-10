using FlightService.Domain.Entities;
using FlightService.Domain.Repositories;
using FlightService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace FlightService.Infrastructure.Repositories
{
    internal sealed class FlightRepository(AppDbContext dbContext) : IFlightRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public void Add(Flight flight)
        {
            _dbContext.Flights.Add(flight);
        }

        public void Update(Flight flight)
        {
            _dbContext.Flights.Update(flight);
        }

        public void Delete(Flight flight)
        {
            _dbContext.Flights.Remove(flight);
        }

        public async Task<Flight?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Flights
                .Include(x => x.Plane)
                .Include(x => x.DepartureDetail)
                .Include(x => x.ArrivalDetail)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Flight>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Flights
                .Include(x => x.Plane)
                .Include(x => x.DepartureDetail)
                .Include(x => x.ArrivalDetail).ToListAsync(cancellationToken);
        }

        public async Task<string> GetLastNumber(CancellationToken cancellationToken)
        {
            return await _dbContext.Flights
                .OrderByDescending(x => x.CreatedOfUtc)
                .Select(x => x.Number.Value)
                .FirstOrDefaultAsync(cancellationToken) ?? string.Empty;
        }
    }
}
