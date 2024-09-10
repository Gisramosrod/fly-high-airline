using System.Text.Json.Serialization;

namespace AviationFleetService.Api.Contracts.Seats.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SeatClass
    {
        Main,
        PremiumEconomy,
        Premium
    }
}
