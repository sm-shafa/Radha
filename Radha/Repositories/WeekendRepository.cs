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
            var Weekend = await connection.QuerySingleOrDefaultAsync<Weekend>(query, new { id });
            return Weekend;
        }
    }
}