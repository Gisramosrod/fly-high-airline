using FlightService.Domain.Entities;

namespace FlightService.Domain.Repositories {

    public interface IFlightDetailRepository {

        void Add(FlightDetail flightDetail);

        void Delete(FlightDetail flightDetail);

        void Update(FlightDetail flightDetail);
    }
}
