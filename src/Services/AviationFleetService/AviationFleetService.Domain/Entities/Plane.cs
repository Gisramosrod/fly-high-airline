using AviationFleetService.Domain.Common;
using AviationFleetService.Domain.Enums;
using AviationFleetService.Domain.Errors;
using ErrorOr;
using System.Security;

namespace AviationFleetService.Domain.Entities
{
    public class Plane : Entity
    {
        public string Number { get; private set; } = string.Empty;
        public string Model { get; private set; } = string.Empty;
        public string Manufacturer { get; private set; } = string.Empty;

        private readonly List<Seat> _seats = [];
        public IReadOnlyCollection<Seat> Seats => _seats;

        private readonly List<PlaneService> _planeServices = [];
        public IReadOnlyCollection<PlaneService> PlaneServices => _planeServices;

        private Plane() { }

        public Plane(Guid id, string number, string model, string manufacturer) : base(id)
        {
            Number = number;
            Model = model;
            Manufacturer = manufacturer;
        }

        public void Set(string number, string model, string manufacturer)
        {
            Number = number;
            Model = model;
            Manufacturer = manufacturer;
        }

        public ErrorOr<Seat> AddSeat(SeatClass seatClass, SeatType seatType, string seatNumber)
        {
            if (Seats.Any(x => x.SeatNumber == seatNumber))
            {
                return SeatErrors.NumberNotUnique;
            }

            var seat = new Seat(Guid.NewGuid(), seatNumber, seatClass, seatType);
            _seats.Add(seat);

            return seat;
        }

        public ErrorOr<Seat> UpdateSeat(Guid seatId, SeatClass seatClass, SeatType seatType, string seatNumber)
        {
            var seat = _seats.Where(x => x.Id == seatId).FirstOrDefault();
            if (seat is null)
            {
                return SeatErrors.NotFound(Id, seatId);
            }

            if (seat.SeatNumber != seatNumber &&
                Seats.Any(x => x.SeatNumber == Number && x.Id != seat.Id))
            {
                return SeatErrors.NumberNotUnique;
            }

            seat.Set(seatNumber, seatClass, seatType);
            return seat;
        }

        public bool RemoveSeat(Seat seat)
        {
            return _seats.Remove(seat);
        }

        public ErrorOr<Success> AddPlaneService(PlaneService planeService)
        {
            if (PlaneServices.Any(x => x.Id == planeService.Id))
            {
                return PlaneErrors.AlreadyHasPlaneService(planeService.Id);
            }

            _planeServices.Add(planeService);

            return Result.Success;
        }

        public ErrorOr<Success> RemovePlaneService(Guid planeServiceId)
        {
            var planeService = _planeServices.Where(x => x.Id == planeServiceId).FirstOrDefault();
            if (planeService is null)
            {
                return PlaneErrors.DoesNotHasPlaneService(planeServiceId);
            }

            _planeServices.Remove(planeService);
            return Result.Success;
        }
    }
}
