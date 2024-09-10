using FlightService.Domain.Common;

namespace FlightService.Domain.Entities
{
    public class Plane : Entity
    {
        public string Number { get; private set; } = string.Empty;
        public string Model { get; private set; } = string.Empty;

        private readonly List<Seat> _seats = [];
        public IReadOnlyCollection<Seat> Seats => _seats;

        private readonly List<PlaneService> _services = [];
        public IReadOnlyCollection<PlaneService> PlaneServices => _services;

        private Plane() { }

        public Plane(Guid id, string number, string model, List<PlaneService> planeServices, List<Seat> seats) : base(id)
        {
            Number = number;
            Model = model;
            _services = planeServices;
            _seats = seats;
        }
    }
}
