using FlightService.Domain.Entities;

namespace FlightService.Domain.Repositories {

    public interface IFlightRepository {

        Task<Flight?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Flight>> ListAsync(CancellationToken cancellationToken);

        void Add(Flight flight);

        void Delete(Flight flight);

        void Update(Flight flight);

        Task<string> GetLastNumber(CancellationToken cancellationToken);
    }
}
