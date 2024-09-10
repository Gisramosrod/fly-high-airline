using ErrorOr;

namespace FlightService.Domain.Errors {

    public static class FlightErrors {

        public static Error NotFound(Guid id) => Error.NotFound("FlightErrors.NotFound", $"The flight with the Id = {id} was not found");

        public static readonly Error ArrivalScheduleDateBeforeDepartureScheduleDate = Error.Validation(
                "FlightErrors.ArrivalScheduleDateBeforeDepartureScheduleDate",
                "The flight cannot have an arrival schedule date before the departure schedule date");

        public static readonly Error NoSeats = Error.Validation("FlightErrors.NoSeats", "The flight must have seats, and the assigned plane has no seats");

        public static readonly Error CannotChangePlaneWithDifferentModel = Error.Validation(
            "FlightErrors.CannotChangePlaneWithDifferentModel",
            "Cannot change the plane of a flight created with a plane of a different model");
    }
}
