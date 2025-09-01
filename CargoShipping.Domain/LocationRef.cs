namespace CargoShipping.Domain;

public record LocationRef(string UnLocode, string Name)
{
    public override string ToString() => $"{UnLocode} - {Name}";
}