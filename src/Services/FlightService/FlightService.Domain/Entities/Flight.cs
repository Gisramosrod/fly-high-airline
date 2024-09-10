using ErrorOr;
using FlightService.Domain.Common;
using FlightService.Domain.Enums;
using FlightService.Domain.Errors;
using FlightService.Domain.ValueObjects;

namespace FlightService.Domain.Entities
{
    public class Flight : Entity, IAuditableEntity
    {
        public FlightNumber Number { get; private set; } = null!;
        public FlightStatus Status { get; private set; }
        public TimeSpan Duration { get; private set; }
        public Plane Plane { get; private set; } = null!;
        public Guid PlaneId { get; }

        private readonly List<FlightSeat> _flightSeats = [];
        public IReadOnlyCollection<FlightSeat> FlightSeats => _flightSeats;
        public FlightDetail DepartureDetail { get; private set; } = null!;
        public Guid DepartureDetailId { get; }
        public FlightDetail ArrivalDetail { get; private set; } = null!;
        public Guid ArrivalDetailId { get; }
        public DateTime CreatedOfUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }

        private Flight() { }

        private Flight(
            Guid id,
            FlightNumber number,
            TimeSpan duration,
            Plane plane,
            List<FlightSeat> flightSeats,
            FlightDetail departureDetail,
            FlightDetail arrivalDetail)
            : base(id)
        {

            Number = number;
            Status = FlightStatus.Scheduled;
            Duration = duration;
            Plane = plane;
            _flightSeats = flightSeats;
            DepartureDetail = departureDetail;
            ArrivalDetail = arrivalDetail;
        }

        public static ErrorOr<Flight> Create(
            Guid id,
            FlightNumber number,
            Plane plane,
            DateTime departureScheduleDate,
            Airport departureAirport,
            DateTime arrivalScheduleDate,
            Airport arrivalAirport)
        {

            if (departureScheduleDate > arrivalScheduleDate)
            {
                return FlightErrors.ArrivalScheduleDateBeforeDepartureScheduleDate;
            }

            var departureDetailResult = FlightDetail.Create(
                Guid.NewGuid(),
                FlightDetailType.Departure,
                departureScheduleDate,
                departureAirport) ;

            if (departureDetailResult.IsError)
            {
                return departureDetailResult.Errors;
            }

            var departureDetail = departureDetailResult.Value;

            var arrivalDetailResult = FlightDetail.Create(
                Guid.NewGuid(),
                FlightDetailType.Arrival,
                arrivalScheduleDate,
                arrivalAirport);

            if (arrivalDetailResult.IsError)
            {
                return departureDetailResult.Errors;
            }

            var arrivalDetail = arrivalDetailResult.Value;

            var duration = arrivalScheduleDate - departureScheduleDate;

            if (plane.Seats.Count == 0)
            {
                return FlightErrors.NoSeats;
            }

            var flightSeats = plane.Seats.Select(
                x => new FlightSeat(
                    Guid.NewGuid(),
                    x.SeatClass,
                    x.SeatNumber,
                    id)).ToList();

            return new Flight(
                id,
                number,
                duration,
                plane,
                flightSeats,
                departureDetail,
                arrivalDetail);
        }

        public ErrorOr<Success> SetPlane(Plane plane)
        {
            if (plane.Model != Plane.Model)
            {
                return FlightErrors.CannotChangePlaneWithDifferentModel;
            }

            Plane = plane;

            return Result.Success;
        }

        public void SetStatus(FlightStatus status)
        {
            Status = status;
        }

        public ErrorOr<Success> SetFlighDetail(
            FlightDetailType type,
            DateTime scheduleDate,
            DateTime actualDate,
            Airport airport)
        {
            var flightDetail =
                type == FlightDetailType.Departure
                ? DepartureDetail
                : ArrivalDetail;

            var setFlightResult = flightDetail.Set(scheduleDate, actualDate, airport);

            if (setFlightResult.IsError)
            {
                return setFlightResult.Errors;
            }
            return Result.Success;
        }
    }
}
