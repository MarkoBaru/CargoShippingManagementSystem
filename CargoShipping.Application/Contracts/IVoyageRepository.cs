using CargoShipping.Domain;

namespace CargoShipping.Application.Contracts;

public interface IVoyageRepository
{
    Task<Voyage?> GetByVoyageNumberAsync(string voyageNumber);
    Task<IEnumerable<Voyage>> GetAllAsync();
    Task AddAsync(Voyage voyage);
}