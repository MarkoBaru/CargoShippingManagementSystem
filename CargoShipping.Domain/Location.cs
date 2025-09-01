namespace CargoShipping.Domain;

public class Location
{
    public string UnLocode { get; private set; }
    public string Name { get; private set; }

    public Location(string unLocode, string name)
    {
        UnLocode = unLocode ?? throw new ArgumentNullException(nameof(unLocode));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    // Für Entity Framework
    protected Location() { }

    public LocationRef ToLocationRef() => new(UnLocode, Name);

    public override bool Equals(object? obj) =>
        obj is Location other && UnLocode == other.UnLocode;

    public override int GetHashCode() => UnLocode.GetHashCode();
}