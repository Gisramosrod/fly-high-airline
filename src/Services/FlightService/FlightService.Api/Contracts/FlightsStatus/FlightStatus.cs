using System.Text.Json.Serialization;

namespace FlightService.Api.Contracts.FlightsStatus {

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FlightStatus {
        Scheduled,
        OnTime,
        Delayed,
        Departed,
        InAir,
        Landed,
        Arrived,
        Cancelled
    }
}

