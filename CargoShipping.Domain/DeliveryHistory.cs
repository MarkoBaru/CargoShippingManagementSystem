namespace CargoShipping.Domain;

public class DeliveryHistory
{
    public List<HandlingEvent> Events { get; private set; } = new();

    public DeliveryHistory() { }

    public DeliveryHistory(IEnumerable<HandlingEvent> events)
    {
        Events = events?.OrderBy(e => e.CompletionTime).ToList() ?? new List<HandlingEvent>();
    }

    public void AddEvent(HandlingEvent handlingEvent)
    {
        Events.Add(handlingEvent);
        Events = Events.OrderBy(e => e.CompletionTime).ToList();
    }

    public HandlingEvent? MostRecentEvent => Events.LastOrDefault();

    public IEnumerable<HandlingEvent> GetEventsAt(LocationRef location) =>
        Events.Where(e => e.Location.UnLocode == location.UnLocode);
}