using CargoShipping.Domain;

namespace CargoShipping.Application.Contracts;

public interface ICargoRepository
{
    Task<Cargo?> GetByTrackingIdAsync(TrackingId trackingId);
    Task<IEnumerable<Cargo>> GetAllAsync();
    Task AddAsync(Cargo cargo);
    Task UpdateAsync(Cargo cargo);
    Task DeleteAsync(TrackingId trackingId);
}