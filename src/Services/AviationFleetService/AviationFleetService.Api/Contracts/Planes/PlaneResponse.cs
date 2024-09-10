using AviationFleetService.Api.Contracts.PlaneServices;
using AviationFleetService.Api.Contracts.Seats;

namespace AviationFleetService.Api.Contracts.Planes
{
    public sealed record PlaneResponse(
        Guid Id,
        string Number,
        string Model,
        List<SeatResponse> Seats,
        List<PlaneServiceReponse> Services);
}
