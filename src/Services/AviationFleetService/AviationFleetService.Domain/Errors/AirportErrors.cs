using ErrorOr;

namespace AviationFleetService.Domain.Errors
{
    public static class AirportErrors
    {
        public static readonly Error NameNotUnique = Error.Validation("AirportErrors.NameNotUnique", "The name is not unique");
        public static Error NotFound(Guid id) => Error.NotFound("AirportErrors.NotFound", $"The airport with the Id = {id} was not found");
    }
}
