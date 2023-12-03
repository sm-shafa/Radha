namespace Radha.Repositories;

public interface IUnitOfRepository
{
    public ICountryRepository Countries { get; }
    public IHolidayRepository Holidays { get; }
    public IWeekendRepository Weekends { get; }

}