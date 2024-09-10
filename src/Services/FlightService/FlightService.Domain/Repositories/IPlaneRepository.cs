using FlightService.Domain.Entities;

namespace FlightService.Domain.Repositories {

    public interface IPlaneRepository {

        Task<Plane?> GetByIdAsync(Guid id, CancellationToken cancellationToken); 
    }
}
