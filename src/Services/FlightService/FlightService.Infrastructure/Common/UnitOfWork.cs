using FlightService.Domain.Repositories;

namespace FlightService.Infrastructure.Common {

    internal class UnitOfWork(AppDbContext dbContext) : IUnitOfWork {

        private readonly AppDbContext _dbContext = dbContext;

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) {

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
