using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Repositories;
using AviationFleetService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace AviationFleetService.Infrastructure.Repositories
{
    internal sealed class PlaneRepository(AppDbContext dbContext) : IPlaneRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public void Add(Plane plane)
        {
            _dbContext.Planes.Add(plane);
        }

        public void Delete(Plane plane)
        {
            _dbContext.Planes.Remove(plane);
        }

        public async Task<Plane?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Planes
                .Where(x => x.Id == id)
                .Include(x => x.Seats)
                .Include(x => x.PlaneServices)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsNumberUniqueAsync(string number, CancellationToken cancellationToken)
        {
            return !await _dbContext.Planes.AnyAsync(x => x.Number == number, cancellationToken);
        }

        public Task<List<Plane>> ListAsync(CancellationToken cancellationToken)
        {
            return _dbContext.Planes
                .Include(x => x.Seats)
                .Include(x => x.PlaneServices)
                .ToListAsync(cancellationToken);
        }

        public void Update(Plane plane)
        {
            _dbContext.Planes.Update(plane);
        }
    }
}
