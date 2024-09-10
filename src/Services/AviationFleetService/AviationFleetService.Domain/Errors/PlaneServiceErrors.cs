using ErrorOr;

namespace AviationFleetService.Domain.Errors
{
    public static class PlaneServiceErrors
    {
        public static readonly Error NameNotUnique = Error.Validation("PlaneServiceErrors.NameNotUnique", "The name is not unique");

        public static readonly Error DescriptionNotUnique = Error.Validation("PlaneServiceErrors.DescriptionNotUnique", "The description is not unique");

        public static Error NotFound(Guid id) => Error.NotFound("PlaneServiceErrors.NotFound", $"The plane service with the Id = {id} was not found");

    }
}
