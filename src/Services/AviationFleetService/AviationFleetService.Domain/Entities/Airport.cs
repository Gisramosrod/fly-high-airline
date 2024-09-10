using AviationFleetService.Domain.Common;

namespace AviationFleetService.Domain.Entities
{
    public class Airport : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public bool IsInternational { get; private set; }

        private Airport() { }

        public Airport(Guid id, string name, string country, string city, bool isInternational) : base(id)
        {
            Name = name;
            Country = country;
            City = city;
            IsInternational = isInternational;
        }

        public void Set(string name, string country, string city, bool isInternational)
        {
            Name = name;
            Country = country;
            City = city;
            IsInternational = isInternational;
        }
    }
}
