using CargoShipping.Domain;

namespace CargoShipping.Application.Contracts;

public interface ILocationRepository
{
    Task<Location?> GetByUnLocodeAsync(string unLocode);
    Task<IEnumerable<Location>> GetAllAsync();
    Task AddAsync(Location location);
}