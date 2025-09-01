namespace CargoShipping.Domain;

public record TrackingId(string Value)
{
    public static implicit operator string(TrackingId trackingId) => trackingId.Value;
    public static implicit operator TrackingId(string value) => new(value);
    
    public override string ToString() => Value;
}