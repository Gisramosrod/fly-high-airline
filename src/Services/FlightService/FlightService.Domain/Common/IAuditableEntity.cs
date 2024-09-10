namespace FlightService.Domain.Common
{
    public interface IAuditableEntity
    {
        DateTime CreatedOfUtc { get; set; }

        DateTime? UpdatedOnUtc { get; set; }
    }
}

