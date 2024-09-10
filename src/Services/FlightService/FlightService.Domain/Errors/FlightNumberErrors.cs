using ErrorOr;

namespace FlightService.Domain.Errors
{
    public static class FlightNumberErrors
    {
        public static readonly Error Empty = Error.Validation("FlightNumberErrors.Empty", "The flight number cannot be empty");

        public static readonly Error InvalidLenght = Error.Validation("FlightNumberErrors.InvalidLenght", "The flight number must have 4 digits");

        public static readonly Error InvalidFormat = Error.Validation("FlightNumberErrors.InvalidFormat", "The flight number digits must only have numbers");
    }
}
