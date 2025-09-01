namespace CargoShipping.Domain;

public class Cargo
{
    public TrackingId TrackingId { get; private set; }
    public RouteSpecification RouteSpec { get; private set; }
    public Itinerary? Itinerary { get; private set; }
    public Delivery Delivery { get; private set; }
    public DeliveryHistory DeliveryHistory { get; private set; }

    public Cargo(TrackingId trackingId, RouteSpecification routeSpec)
    {
        TrackingId = trackingId ?? throw new ArgumentNullException(nameof(trackingId));
        RouteSpec = routeSpec ?? throw new ArgumentNullException(nameof(routeSpec));
        Delivery = new Delivery(null, TransportStatus.NOT_RECEIVED, false);
        DeliveryHistory = new DeliveryHistory();
    }

    // Für Entity Framework
    protected Cargo() { }

    public void AssignToRoute(Itinerary itinerary)
    {
        Itinerary = itinerary ?? throw new ArgumentNullException(nameof(itinerary));
        
        // Validierung: Itinerary muss zur RouteSpecification passen
        if (itinerary.InitialDepartureLocation?.UnLocode != RouteSpec.Origin.UnLocode)
            throw new InvalidOperationException("Itinerary origin must match route specification origin");
            
        if (itinerary.FinalArrivalLocation?.UnLocode != RouteSpec.Destination.UnLocode)
            throw new InvalidOperationException("Itinerary destination must match route specification destination");
            
        if (itinerary.FinalArrivalTime > RouteSpec.ArrivalDeadline)
            throw new InvalidOperationException("Itinerary arrival time exceeds deadline");

        UpdateDeliveryStatus();
    }

    public void HandleEvent(HandlingEvent handlingEvent)
    {
        DeliveryHistory.AddEvent(handlingEvent);
        UpdateDeliveryStatus();
    }

    private void UpdateDeliveryStatus()
    {
        var mostRecentEvent = DeliveryHistory.MostRecentEvent;
        
        if (mostRecentEvent == null)
        {
            Delivery.UpdateStatus(null, TransportStatus.NOT_RECEIVED, false);
            return;
        }

        var location = mostRecentEvent.Location;
        var transportStatus = DetermineTransportStatus(mostRecentEvent);
        var misdirected = IsMisdirected(mostRecentEvent);
        var eta = CalculateEta();
        var nextActivity = DetermineNextExpectedActivity();

        Delivery.UpdateStatus(location, transportStatus, misdirected, eta, nextActivity);
    }

    private TransportStatus DetermineTransportStatus(HandlingEvent mostRecentEvent)
    {
        return mostRecentEvent.Type switch
        {
            HandlingType.RECEIVE => TransportStatus.IN_PORT,
            HandlingType.LOAD => TransportStatus.ONBOARD_CARRIER,
            HandlingType.UNLOAD => TransportStatus.IN_PORT,
            HandlingType.CLAIM => TransportStatus.CLAIMED,
            HandlingType.CUSTOMS => TransportStatus.IN_PORT,
            _ => TransportStatus.UNKNOWN
        };
    }

    private bool IsMisdirected(HandlingEvent mostRecentEvent)
    {
        if (Itinerary == null) return true;

        // Einfache Implementierung - könnte erweitert werden
        var expectedLocation = GetExpectedLocationForEvent(mostRecentEvent);
        return expectedLocation != null && 
               expectedLocation.UnLocode != mostRecentEvent.Location.UnLocode;
    }

    private LocationRef? GetExpectedLocationForEvent(HandlingEvent handlingEvent)
    {
        if (Itinerary == null) return null;

        // Vereinfachte Logik - könnte basierend auf Zeitpunkt und Event-Typ verfeinert werden
        return handlingEvent.Type switch
        {
            HandlingType.RECEIVE => RouteSpec.Origin,
            HandlingType.CLAIM => RouteSpec.Destination,
            _ => null
        };
    }

    private DateTime? CalculateEta()
    {
        return Itinerary?.FinalArrivalTime;
    }

    private string? DetermineNextExpectedActivity()
    {
        if (Itinerary == null) return "Assign to route";
        
        var mostRecentEvent = DeliveryHistory.MostRecentEvent;
        if (mostRecentEvent == null) return "Receive cargo";

        return mostRecentEvent.Type switch
        {
            HandlingType.RECEIVE => "Load onto carrier",
            HandlingType.LOAD => "Unload at destination",
            HandlingType.UNLOAD when mostRecentEvent.Location.UnLocode == RouteSpec.Destination.UnLocode => "Claim cargo",
            HandlingType.UNLOAD => "Load onto next carrier",
            HandlingType.CUSTOMS => "Continue transport",
            HandlingType.CLAIM => "Delivery complete",
            _ => "Unknown"
        };
    }

    public bool IsDelivered => DeliveryHistory.MostRecentEvent?.Type == HandlingType.CLAIM &&
                              DeliveryHistory.MostRecentEvent?.Location.UnLocode == RouteSpec.Destination.UnLocode;

    public override bool Equals(object? obj) =>
        obj is Cargo other && TrackingId.Value == other.TrackingId.Value;

    public override int GetHashCode() => TrackingId.GetHashCode();
}