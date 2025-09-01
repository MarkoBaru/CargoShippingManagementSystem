namespace CargoShipping.Domain;

public class Itinerary
{
    public List<Leg> Legs { get; private set; } = new();

    public Itinerary() { }

    public Itinerary(IEnumerable<Leg> legs)
    {
        Legs = legs?.ToList() ?? throw new ArgumentNullException(nameof(legs));
        ValidateItinerary();
    }

    public void AddLeg(Leg leg)
    {
        Legs.Add(leg);
        ValidateItinerary();
    }

    private void ValidateItinerary()
    {
        if (Legs.Count == 0) return;

        for (int i = 0; i < Legs.Count - 1; i++)
        {
            var current = Legs[i];
            var next = Legs[i + 1];

            if (current.UnloadLocation.UnLocode != next.LoadLocation.UnLocode)
                throw new InvalidOperationException("Legs must be connected - unload location must match next load location");

            if (current.UnloadTime >= next.LoadTime)
                throw new InvalidOperationException("There must be time between legs");
        }
    }

    public LocationRef? InitialDepartureLocation => Legs.FirstOrDefault()?.LoadLocation;
    public LocationRef? FinalArrivalLocation => Legs.LastOrDefault()?.UnloadLocation;
    public DateTime? FinalArrivalTime => Legs.LastOrDefault()?.UnloadTime;
}