using AviationFleetService.Domain.Common;
using AviationFleetService.Domain.Enums;

namespace AviationFleetService.Domain.Entities
{
    public class Seat : Entity
    {
        public string SeatNumber { get; private set; } = string.Empty;
        public SeatClass SeatClass { get; private set; }
        public SeatType SeatType { get; private set; }

        private Seat() { }

        internal Seat(Guid id, string seatNumber, SeatClass seatClass, SeatType seatType) : base(id)
        {
            SeatClass = seatClass;
            SeatNumber = seatNumber;
            SeatType = seatType;
        }

        internal void Set(string seatNumber, SeatClass seatClass, SeatType seatType)
        {
            SeatClass = seatClass;
            SeatNumber = seatNumber;
            SeatType = seatType;
        }
    }
}
