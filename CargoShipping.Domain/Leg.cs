namespace CargoShipping.Domain;

public class Leg
{
    public VoyageRef Voyage { get; private set; }
    public LocationRef LoadLocation { get; private set; }
    public LocationRef UnloadLocation { get; private set; }
    public DateTime LoadTime { get; private set; }
    public DateTime UnloadTime { get; private set; }

    public Leg(VoyageRef voyage, LocationRef loadLocation, LocationRef unloadLocation, 
               DateTime loadTime, DateTime unloadTime)
    {
        Voyage = voyage ?? throw new ArgumentNullException(nameof(voyage));
        LoadLocation = loadLocation ?? throw new ArgumentNullException(nameof(loadLocation));
        UnloadLocation = unloadLocation ?? throw new ArgumentNullException(nameof(unloadLocation));
        LoadTime = loadTime;
        UnloadTime = unloadTime;

        if (loadTime >= unloadTime)
            throw new ArgumentException("Load time must be before unload time");
    }

    // Für Entity Framework
    protected Leg() { }
}