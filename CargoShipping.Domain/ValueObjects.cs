namespace CargoShipping.Domain;

public record VoyageRef(string VoyageNumber);

public record RouteSpecification(
    LocationRef Origin,
    LocationRef Destination,
    DateTime ArrivalDeadline
);