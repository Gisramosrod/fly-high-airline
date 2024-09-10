using FlightService.Domain.Common;
using FlightService.Domain.Enums;

namespace FlightService.Domain.Entities
{
    public class Seat : Entity
    {
        public SeatClass SeatClass { get; private set; }
        public string SeatNumber { get; private set; } = string.Empty;

        protected Seat() { }

        public Seat(Guid id, SeatClass seatClass, string seatNumber) : base(id)
        {
            SeatClass = seatClass;
            SeatNumber = seatNumber;
        }
    }
}
