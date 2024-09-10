namespace AviationFleetService.Api.Contracts.Planes
{
    public sealed record UpdatePlaneRequest(
        string Number,
        string Model,
        string Manufacturer);
}
