using AviationFleetService.Domain.Entities;

namespace AviationFleetService.Domain.Repositories
{
    public interface IPlaneServiceRepository
    {
        Task<PlaneService?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<PlaneService>> ListAsync(CancellationToken cancellationToken);
        void Add(PlaneService planeService);
        void Update(PlaneService planeService);
        void Delete(PlaneService planeService);
        Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);
        Task<bool> IsDescriptionUniqueAsync(string description, CancellationToken cancellationToken);

    }
}
