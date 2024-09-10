using ErrorOr;

namespace FlightService.Domain.Errors {

    public static class AirportErrors {

        public static Error NotFound(Guid id) => Error.NotFound("AirportErrors.NotFound", $"The airport with the Id = {id} was not found");
    }
}
