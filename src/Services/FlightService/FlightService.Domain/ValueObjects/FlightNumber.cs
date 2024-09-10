using ErrorOr;
using FlightService.Domain.Errors;

namespace FlightService.Domain.ValueObjects
{
    public record FlightNumber
    {
        private static readonly string _code = "FH";
        public string Value { get; init; } = string.Empty;

        private FlightNumber() { }

        private FlightNumber(string value) => Value = value;

        public static ErrorOr<FlightNumber> Create(string flightDigits)
        {
            if (string.IsNullOrWhiteSpace(flightDigits))
            {
                return FlightNumberErrors.Empty;
            }

            if (flightDigits.Length != 4)
            {
                return FlightNumberErrors.InvalidLenght;
            }

            if (!flightDigits.All(char.IsDigit))
            {
                return FlightNumberErrors.InvalidFormat;
            }

            var flightNumber = _code + flightDigits;
            return new FlightNumber(flightNumber);
        }
    }
}
