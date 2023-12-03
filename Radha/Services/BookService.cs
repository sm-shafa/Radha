using Radha.Dtos;
using Radha.Repositories;

namespace Radha.Services;

public class BookService : IBookService
{
    private readonly IUnitOfRepository _unitOfRepository;

    public BookService(IUnitOfRepository unitOfRepository)
    {
        _unitOfRepository = unitOfRepository;
    }

    public async Task<BookCheckDto> Calculate(DateTime checkout, DateTime checkin, int countryId)
    {
        BookCheckDto penaltyDayDto = new BookCheckDto();
        int businessDays =
            await CalculateBusinessDays(DateOnly.FromDateTime(checkout), DateOnly.FromDateTime(checkin), countryId);
        decimal penalty = await CalculatePenalty(businessDays, countryId);
        var country = await _unitOfRepository.Countries.GetCountry(countryId);

        penaltyDayDto.Penalty = penalty;
        penaltyDayDto.BusinessDays = businessDays;
        penaltyDayDto.CurrencySign = country.CurrencySign;
        return penaltyDayDto;
    }

    private async Task<int> CalculateBusinessDays(DateOnly dateCheckedOut, DateOnly dateCheckedIn, int countryId)
    {
        var holiday = await _unitOfRepository.Holidays.GetCountryHoliday(countryId);
        var weekendDay = await _unitOfRepository.Weekends.GetCountryWeekend(countryId);

        List<DateOnly> holidays = holiday.ToList().Select(d => DateOnly.FromDateTime(d.Date)).ToList();
        var weekendDays = weekendDay.ToList().Select(w => w.Day);

        int businessDays = 0;
        for (DateOnly date = dateCheckedIn; date <= dateCheckedOut; date = date.AddDays(1))
        {
            if (!weekendDays.Contains(date.DayOfWeek.ToString()) && !holidays.Contains(date))
            {
                businessDays++;
            }
        }

        return businessDays;
    }


    private async Task<decimal> CalculatePenalty(int businessDays, int countryId)
    {
        var country = await _unitOfRepository.Countries.GetCountry(countryId);
        string currencyCode = country.CurrencySign;
        decimal penaltyAmount = businessDays * 5.00m;
        string penaltyFormatted = currencyCode + penaltyAmount.ToString("F2");

        return penaltyAmount;
    }
}