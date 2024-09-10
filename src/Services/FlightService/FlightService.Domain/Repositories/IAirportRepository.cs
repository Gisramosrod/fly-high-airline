using FlightService.Domain.Entities;

namespace FlightService.Domain.Repositories
{
    public interface IAirportRepository
    {
        void Add(Airport airport);
        void Update(Airport airport);
        void Delete(Airport airport);
        Task<Airport?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
