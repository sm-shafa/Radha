using Backend_Assessment.Models;
using Dapper;
using Radha.Data;

namespace Radha.Repositories;

public class HolidayRepository: IHolidayRepository
{
    private readonly BookContext _context;
    public HolidayRepository(BookContext context)
    {
        _context = context;
    }

    public async Task<Holiday> GetHoliday(int id)
    {
        var query = "SELECT * FROM Holidays WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var holiday = await connection.QuerySingleOrDefaultAsync<Holiday>(query, new { id });
            return holiday;
        }
    }

    public async Task<IEnumerable<Holiday>> GetCountryHoliday(int countryId)
    {
        var query = "SELECT * FROM Holidays WHERE countryId = @countryId";
        using (var connection = _context.CreateConnection())
        {
            var holiday = await connection.QueryAsync<Holiday>(query, new { countryId });
            return holiday.ToList();
        }
    }
}