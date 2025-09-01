namespace CargoShipping.Domain;

public class Delivery
{
    public LocationRef? LastKnownLocation { get; private set; }
    public TransportStatus TransportStatus { get; private set; }
    public bool Misdirected { get; private set; }
    public DateTime? Eta { get; private set; }
    public string? NextExpectedActivity { get; private set; }

    public Delivery(LocationRef? lastKnownLocation, TransportStatus transportStatus, 
                   bool misdirected, DateTime? eta = null, string? nextExpectedActivity = null)
    {
        LastKnownLocation = lastKnownLocation;
        TransportStatus = transportStatus;
        Misdirected = misdirected;
        Eta = eta;
        NextExpectedActivity = nextExpectedActivity;
    }

    // Für Entity Framework
    protected Delivery() { }

    public void UpdateStatus(LocationRef? lastKnownLocation, TransportStatus transportStatus, 
                           bool misdirected, DateTime? eta = null, string? nextExpectedActivity = null)
    {
        LastKnownLocation = lastKnownLocation;
        TransportStatus = transportStatus;
        Misdirected = misdirected;
        Eta = eta;
        NextExpectedActivity = nextExpectedActivity;
    }

    public bool IsOnTrack => !Misdirected && TransportStatus != TransportStatus.UNKNOWN;
}