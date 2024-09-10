using ErrorOr;
using FlightService.Domain.Repositories;
using FlightService.Domain.ValueObjects;

namespace FlightService.Domain.Services
{
    public sealed class FlightNumberService(IFlightRepository flightRepository) : IFlightNumberService
    {
        private readonly IFlightRepository _flightRepository = flightRepository;

        public async Task<ErrorOr<FlightNumber>> GenerateFlightNumber(CancellationToken cancellationToken)
        {
            var flightDigits = 0;
            var lastFlightNumber = await _flightRepository.GetLastNumber(cancellationToken);

            if (!string.IsNullOrWhiteSpace(lastFlightNumber))
            {
                var lastFlightDigits = int.Parse(lastFlightNumber.Substring(2));
                if (lastFlightDigits < 9999)
                {
                    flightDigits = lastFlightDigits++;
                }
            }

            var flightNumberResult = FlightNumber.Create(flightDigits.ToString("D4"));
            if (flightNumberResult.IsError)
            {
                return flightNumberResult.Errors;
            }

            return flightNumberResult.Value;
        }
    }
}
