using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Repositories;
using AviationFleetService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace AviationFleetService.Infrastructure.Repositories
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

        public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
        {
            return !await _dbContext.Airports.AnyAsync(x => x.Name == name, cancellationToken);
        }

        public Task<List<Airport>> ListAsync(CancellationToken cancellationToken)
        {
            return _dbContext.Airports.ToListAsync(cancellationToken);
        }

        public void Update(Airport airport)
        {
            _dbContext.Airports.Update(airport);
        }
    }
}
