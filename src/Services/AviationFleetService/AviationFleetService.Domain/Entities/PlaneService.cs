using AviationFleetService.Domain.Common;

namespace AviationFleetService.Domain.Entities
{
    public class PlaneService : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        private readonly List<Plane> _planes = [];
        public IReadOnlyCollection<Plane> Planes => _planes;

        private PlaneService() { }

        public PlaneService(Guid id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }

        public void Set(string name, string description) 
        {
            Name = name;
            Description = description;
        }
    }
}

