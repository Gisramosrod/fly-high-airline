using ErrorOr;

namespace FlightService.Domain.Errors {

    public static class PlaneErrors {

        public static Error NotFound(Guid id) => Error.NotFound("PlaneErrors.NotFound", $"The plane with the Id = {id} was not found");

    }
}
