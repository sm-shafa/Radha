using Radha.Entities;

namespace Radha.Repositories;

public interface ICountryRepository
{
    public Task<Country> GetCountry(int id);
    public Task<Country> GetCountryWithRelationMultipleResults(int id);
}