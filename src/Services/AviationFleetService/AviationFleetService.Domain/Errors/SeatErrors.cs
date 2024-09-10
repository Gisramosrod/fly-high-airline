using ErrorOr;

namespace AviationFleetService.Domain.Errors
{
    public static class SeatErrors
    {
        public static readonly Error NumberNotUnique = Error.Validation("SeatErrors.NumberNotUnique", "The number is not unique");

        public static Error NotFound(Guid planeId, Guid seatId) =>
            Error.NotFound(
                "SeatErrors.NotFound",
                $"The seat with the Id = {seatId} was not found on the plane with Id = {planeId}");

    }
}
