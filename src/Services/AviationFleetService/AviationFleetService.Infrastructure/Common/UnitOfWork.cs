
using AviationFleetService.Domain.Repositories;
using AviationFleetService.Infrastructure.Common;

namespace AviationFleetService.Infrastructure.Common {

    internal class UnitOfWork(AppDbContext dbContext) : IUnitOfWork {

        private readonly AppDbContext _dbContext = dbContext;

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) {

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
