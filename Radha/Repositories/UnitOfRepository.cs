namespace Radha.Repositories;

public class UnitOfRepository : IUnitOfRepository
{
    public ICountryRepository Countries { get; set; }
    public IHolidayRepository Holidays { get; set; }
    public IWeekendRepository Weekends { get; set; }

    public UnitOfRepository(ICountryRepository countries, IHolidayRepository holidays, IWeekendRepository weekends)
    {
        Countries = countries;
        Holidays = holidays;
        Weekends = weekends;
    }
}