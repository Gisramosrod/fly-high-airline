using FlightService.Domain.Entities;
using FlightService.Domain.Repositories;
using FlightService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace FlightService.Infrastructure.Repositories
{
    internal sealed class PlaneRepository(AppDbContext dbContext) : IPlaneRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Plane?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Planes.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
