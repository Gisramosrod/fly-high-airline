using ErrorOr;
using FlightService.Domain.Common;
using FlightService.Domain.Enums;

namespace FlightService.Domain.Entities
{
    public class FlightDetail : Entity
    {
        public FlightDetailType Type { get; private set; }
        public DateTime ScheduleDate { get; private set; }
        public DateTime? ActualDate { get; private set; }
        public Airport Airport { get; private set; } = null!;

        private FlightDetail() { }

        private FlightDetail(
            Guid id,
            FlightDetailType type,
            DateTime scheduleDate,
            Airport airport) : base(id)
        {
            Type = type;
            ScheduleDate = scheduleDate;
            Airport = airport;
        }

        internal static ErrorOr<FlightDetail> Create(
            Guid id,
            FlightDetailType type,
            DateTime scheduleDate,
            Airport airport)
        {

            return new FlightDetail(id, type, scheduleDate, airport);
        }

        internal ErrorOr<Success> Set(
            DateTime scheduleDate,
            DateTime actualDate,
            Airport airport)
        {

            ScheduleDate = scheduleDate;
            ActualDate = actualDate;
            Airport = airport;

            return Result.Success;

        }
    }
}
