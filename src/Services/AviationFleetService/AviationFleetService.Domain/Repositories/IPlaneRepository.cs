using AviationFleetService.Domain.Entities;

namespace AviationFleetService.Domain.Repositories
{
    public interface IPlaneRepository
    {
        Task<Plane?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Plane>> ListAsync(CancellationToken cancellationToken);
        void Add(Plane plane);
        void Update(Plane plane);
        void Delete(Plane plane);
        Task<bool> IsNumberUniqueAsync(string number, CancellationToken cancellationToken);
    }
}
