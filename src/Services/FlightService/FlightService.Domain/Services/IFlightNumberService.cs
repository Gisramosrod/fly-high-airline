using ErrorOr;
using FlightService.Domain.ValueObjects;

namespace FlightService.Domain.Services {

    public interface IFlightNumberService {

        Task<ErrorOr<FlightNumber>> GenerateFlightNumber(CancellationToken cancellationToken);
    }
}
