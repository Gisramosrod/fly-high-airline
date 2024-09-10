using FlightService.Domain.Common;

namespace FlightService.Domain.Entities
{
    public class Airport : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;

        private Airport() { }

        public Airport(Guid id, string name, string country, string city) : base(id)
        {
            Name = name;
            Country = country;
            City = city;
        }

        public void Set(string name, string country, string city)
        {
            Name = name;
            Country = country;
            City = city;
        }
    }
}
