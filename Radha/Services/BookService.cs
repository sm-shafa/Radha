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

    public async Task<BookCheckDto> Calculate(DateTime checkout, DateTime returnDate, int countryId)
    {
        if (returnDate < checkout)
            throw new Exception("returnDate must be greater than checkout date");
        
        BookCheckDto penaltyDayDto = new BookCheckDto();
        int businessDays =
            await CalculateBusinessDays(DateOnly.FromDateTime(checkout), DateOnly.FromDateTime(returnDate), countryId);
        decimal penalty = await CalculatePenalty(businessDays);
        var country = await _unitOfRepository.Countries.GetCountry(countryId);

        penaltyDayDto.Penalty = penalty;
        penaltyDayDto.BusinessDays = businessDays;
        penaltyDayDto.CurrencySign = country.CurrencySign;
        return penaltyDayDto;
    }

    private async Task<int> CalculateBusinessDays(DateOnly dateCheckedOut, DateOnly dateReturn, int countryId)
    {
        var holiday = await _unitOfRepository.Holidays.GetCountryHoliday(countryId);
        var weekendDay = await _unitOfRepository.Weekends.GetCountryWeekend(countryId);

        List<DateOnly> holidays = holiday.ToList().Select(d => DateOnly.FromDateTime(d.Date)).ToList();
        var weekendDays = weekendDay.ToList().Select(w => w.Day);

        int businessDays = 0;
        for (DateOnly date = dateCheckedOut; date <= dateReturn; date = date.AddDays(1))
        {
            if (!weekendDays.Contains(date.DayOfWeek.ToString()) && !holidays.Contains(date))
            {
                businessDays++;
            }
        }

        return businessDays;
    }


    private async Task<decimal> CalculatePenalty(int businessDays)
    {
        decimal penaltyAmount = 0;
        if (businessDays > 10)
        {
            penaltyAmount = (businessDays - 10) * 5.00m;
        }

        return penaltyAmount;
    }
}