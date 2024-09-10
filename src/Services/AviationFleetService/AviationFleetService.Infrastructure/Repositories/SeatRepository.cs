using AviationFleetService.Domain.Entities;
using AviationFleetService.Domain.Repositories;
using AviationFleetService.Infrastructure.Common;

namespace AviationFleetService.Infrastructure.Repositories
{
    internal sealed class SeatRepository(AppDbContext dbContext) : ISeatRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public void Add(Seat seat)
        {
            _dbContext.Seats.Add(seat);
        }

        public void Delete(Seat seat)
        {
            _dbContext.Seats.Remove(seat);
        }

        public void Update(Seat seat)
        {
            _dbContext.Seats.Update(seat);
        }
    }
}
