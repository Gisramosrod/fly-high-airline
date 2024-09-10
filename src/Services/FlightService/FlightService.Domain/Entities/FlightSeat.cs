using FlightService.Domain.Enums;

namespace FlightService.Domain.Entities
{
    public class FlightSeat : Seat
    {
        public Guid FlightId { get; private set; }
        public bool Available { get; private set; }

        private FlightSeat() { }

        internal FlightSeat(
            Guid id,
            SeatClass seatClass,
            string seatNumber,
            Guid flightId)
            : base(id, seatClass, seatNumber)
        {
            FlightId = flightId;
            Available = true;
        }
    }
}
