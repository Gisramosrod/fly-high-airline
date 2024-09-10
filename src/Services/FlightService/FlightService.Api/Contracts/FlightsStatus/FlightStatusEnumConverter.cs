namespace FlightService.Api.Contracts.FlightsStatus {

    public static class FlightStatusEnumConverter {

        public static Domain.Enums.FlightStatus ToDomainFlightStatus(FlightStatus apiFlightStatus) {
            return (Domain.Enums.FlightStatus)Enum.Parse(
                typeof(Domain.Enums.FlightStatus),
                apiFlightStatus.ToString());
        }

        public static FlightStatus ToDtoFlightStatus(Domain.Enums.FlightStatus FlightStatus) {
            return (FlightStatus)Enum.Parse(
                typeof(FlightStatus),
                FlightStatus.ToString());
        }
    }
}
