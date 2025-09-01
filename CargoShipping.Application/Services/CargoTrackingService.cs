using CargoShipping.Domain;

namespace CargoShipping.Application.Services;

public class CargoTrackingService
{
    public static TransportStatus DetermineTransportStatus(Cargo cargo)
    {
        var mostRecentEvent = cargo.DeliveryHistory.MostRecentEvent;
        
        if (mostRecentEvent == null)
            return TransportStatus.NOT_RECEIVED;

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

    public static bool IsMisdirected(Cargo cargo)
    {
        if (cargo.Itinerary == null) return true;
        
        var mostRecentEvent = cargo.DeliveryHistory.MostRecentEvent;
        if (mostRecentEvent == null) return false;

        // Vereinfachte Logik für Demonstration
        switch (mostRecentEvent.Type)
        {
            case HandlingType.RECEIVE:
                return mostRecentEvent.Location.UnLocode != cargo.RouteSpec.Origin.UnLocode;
            case HandlingType.CLAIM:
                return mostRecentEvent.Location.UnLocode != cargo.RouteSpec.Destination.UnLocode;
            default:
                return false;
        }
    }
}