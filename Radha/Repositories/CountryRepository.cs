using Radha.Data;
using Radha.Entities;

namespace Radha.Repositories;

public class CountryRepository: ICountryRepository
{
    private readonly BookContext _context;
    public CountryRepository(BookContext context)
    {
        _context = context;
    }


}