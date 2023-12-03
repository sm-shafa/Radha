using Backend_Assessment.Models;
using Dapper;
using Radha.Data;

namespace Radha.Repositories;

public class WeekendRepository: IWeekendRepository
{
    private readonly BookContext _context;
    public WeekendRepository(BookContext context)
    {
        _context = context;
    }

    public async Task<Weekend> GetWeekend(int id)
    {
        var query = "SELECT * FROM Weekend WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var weekend = await connection.QuerySingleOrDefaultAsync<Weekend>(query, new { id });
            return weekend;
        }
    }

    public async Task<IEnumerable<Weekend>> GetCountryWeekend(int countryId)
    {
        var query = "SELECT * FROM Weekend WHERE countryId = @countryId";
        using (var connection = _context.CreateConnection())
        {
            var weekend = await connection.QueryAsync<Weekend>(query, new { countryId });
            return weekend.ToList();
        }
    }
}