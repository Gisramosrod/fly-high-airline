using AviationFleetService.Domain.Entities;

namespace AviationFleetService.Domain.Repositories
{
    public interface IAirportRepository
    {
        Task<Airport?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Airport>> ListAsync(CancellationToken cancellationToken);
        void Add(Airport airport);
        void Update(Airport airport);
        void Delete(Airport airport);
        Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);
    }
}
