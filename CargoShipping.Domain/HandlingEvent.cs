namespace CargoShipping.Domain;

public class HandlingEvent
{
    public Guid EventId { get; private set; }
    public HandlingType Type { get; private set; }
    public DateTime CompletionTime { get; private set; }
    public DateTime RegistrationTime { get; private set; }
    public LocationRef Location { get; private set; }
    public VoyageRef? Voyage { get; private set; }

    public HandlingEvent(HandlingType type, DateTime completionTime, DateTime registrationTime, 
                        LocationRef location, VoyageRef? voyage = null)
    {
        EventId = Guid.NewGuid();
        Type = type;
        CompletionTime = completionTime;
        RegistrationTime = registrationTime;
        Location = location ?? throw new ArgumentNullException(nameof(location));
        Voyage = voyage;
    }

    // Für Entity Framework
    protected HandlingEvent() { }

    public override bool Equals(object? obj) =>
        obj is HandlingEvent other && EventId == other.EventId;

    public override int GetHashCode() => EventId.GetHashCode();
}