using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Repositories;
using AviationFleetService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace AviationFleetService.Infrastructure.Repositories
{
    internal sealed class PlaneServiceRepository(AppDbContext dbContext) : IPlaneServiceRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public void Add(PlaneService planeService)
        {
            _dbContext.PlaneServices.Add(planeService);
        }

        public void Delete(PlaneService planeService)
        {
            _dbContext.PlaneServices.Remove(planeService);
        }

        public async Task<PlaneService?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.PlaneServices.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsDescriptionUniqueAsync(string description, CancellationToken cancellationToken)
        {
            return !await _dbContext.PlaneServices.AnyAsync(x => x.Description == description, cancellationToken);
        }

        public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
        {
            return !await _dbContext.PlaneServices.AnyAsync(x => x.Name == name, cancellationToken);
        }

        public async Task<List<PlaneService>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.PlaneServices.ToListAsync(cancellationToken);
        }

        public void Update(PlaneService planeService)
        {
            _dbContext.PlaneServices.Update(planeService);
        }
    }
}
