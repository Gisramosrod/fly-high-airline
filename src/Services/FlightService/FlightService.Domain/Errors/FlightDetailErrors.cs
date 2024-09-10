using ErrorOr;

namespace FlightService.Domain.Errors {

    public static class FlightDetailErrors {

        public static readonly Error TerminalNotAtAirport = Error.Validation(
            "FlightDetailErrors.TerminalNotAtAirport",
            "The flight detail cannot have a terminal that does not belong to the specified airport");

        public static readonly Error GateNotAtAirport = Error.Validation(
            "FlightDetailErrors.GateNotAtAirport",
            "The flight detail cannot have a gate that does not belong to the specified airport");
    }
}
