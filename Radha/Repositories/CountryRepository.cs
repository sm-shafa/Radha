using Backend_Assessment.Models;
using Dapper;
using Radha.Data;
using Radha.Entities;

namespace Radha.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly BookContext _context;

    public CountryRepository(BookContext context)
    {
        _context = context;
    }

    public async Task<Country> GetCountry(int id)
    {
        var query = "SELECT * FROM Countries WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var country = await connection.QuerySingleOrDefaultAsync<Country>(query, new {id});
            return country;
        }
    }

    public async Task<Country> GetCountryWithRelationMultipleResults(int id)
    {
        var query = "SELECT * FROM Countries WHERE Id = @Id;" +
                    "SELECT * FROM Holidays WHERE CountryId = @Id;" +
                    "SELECT * FROM Weekend WHERE CountryId = @Id";
        using (var connection = _context.CreateConnection())
        using (var multi = await connection.QueryMultipleAsync(query, new {id}))
        {
            var country = await multi.ReadSingleOrDefaultAsync<Country>();
            if (country != null)
            {
                country.Holidays = (await multi.ReadAsync<Holiday>()).ToList();
                country.Weekends = (await multi.ReadAsync<Weekend>()).ToList();
            }

            return country;
        }
    }
}