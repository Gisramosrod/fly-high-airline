using AviationFleetService.Domain.Entities;

namespace AviationFleetService.Domain.Repositories
{
    public interface ISeatRepository
    {
        void Add(Seat seat);
        void Update(Seat seat);
        void Delete(Seat seat);
    }
}
