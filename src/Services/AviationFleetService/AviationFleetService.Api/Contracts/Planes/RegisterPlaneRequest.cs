namespace AviationFleetService.Api.Contracts.Planes
{
    public sealed record RegisterPlaneRequest(
        string Number,
        string Model,
        string Manufacturer);
}
