using ErrorOr;

namespace AviationFleetService.Domain.Errors
{
    public static class PlaneErrors
    {
        public static readonly Error NumberNotUnique = Error.Validation("PlaneErrors.NumberNotUnique", "The number is not unique");
        public static Error AlreadyHasPlaneService(Guid planeServiceId) =>
            Error.Conflict(
                "PlaneErrors.AlreadyHasPlaneService",
                $"The plane already has the plane service with Id = {planeServiceId}");

        public static Error DoesNotHasPlaneService(Guid planeServiceId) =>
          Error.Conflict(
              "PlaneErrors.DoesNotHasPlaneService",
              $"The plane does not has the plane service with Id = {planeServiceId}");
        public static Error NotFound(Guid id) => Error.NotFound("PlaneErrors.NotFound", $"The plane with the Id = {id} was not found");

    }
}
