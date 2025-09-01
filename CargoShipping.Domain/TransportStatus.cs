namespace CargoShipping.Domain;

public enum TransportStatus
{
    NOT_RECEIVED,
    IN_PORT,
    ONBOARD_CARRIER,
    CLAIMED,
    UNKNOWN
}