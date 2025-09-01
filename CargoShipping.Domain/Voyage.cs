namespace CargoShipping.Domain;

public class Voyage
{
    public string VoyageNumber { get; private set; }
    public List<Leg> Schedule { get; private set; } = new();

    public Voyage(string voyageNumber)
    {
        VoyageNumber = voyageNumber ?? throw new ArgumentNullException(nameof(voyageNumber));
    }

    // Für Entity Framework
    protected Voyage() { }

    public void AddLeg(Leg leg)
    {
        Schedule.Add(leg);
    }

    public VoyageRef ToVoyageRef() => new(VoyageNumber);

    public override bool Equals(object? obj) =>
        obj is Voyage other && VoyageNumber == other.VoyageNumber;

    public override int GetHashCode() => VoyageNumber.GetHashCode();
}