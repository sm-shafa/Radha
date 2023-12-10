using Backend_Assessment.Models;
using Moq;
using Radha.Entities;
using Radha.Repositories;
using Radha.Services;

namespace TestProject1;

public class BookServiceTests
{
    private readonly Mock<IUnitOfRepository> _mockUnitOfRepository;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _mockUnitOfRepository = new Mock<IUnitOfRepository>();
        _bookService = new BookService(_mockUnitOfRepository.Object);
    }

    [Fact]
    public async Task Calculate_ReturnsExpectedResult()
    {
        // Arrange
        var checkout = new DateTime(2022, 1, 1);
        var returnDate = new DateTime(2022, 1, 10);
        var countryId = 1;
        var expectedPenalty = 0m;
        var expectedBusinessDays = 10;
        var expectedCurrencySign = "$";

        _mockUnitOfRepository.Setup(x => x.Countries.GetCountry(countryId))
            .ReturnsAsync(new Country {CurrencySign = "$"});
        _mockUnitOfRepository.Setup(x => x.Holidays.GetCountryHoliday(countryId)).ReturnsAsync(new List<Holiday>());
        _mockUnitOfRepository.Setup(x => x.Weekends.GetCountryWeekend(countryId)).ReturnsAsync(new List<Weekend>());

        // Act
        var result = await _bookService.Calculate(checkout, returnDate, countryId);

        // Assert
        Assert.Equal(expectedPenalty, result.Penalty);
        Assert.Equal(expectedBusinessDays, result.BusinessDays);
        Assert.Equal(expectedCurrencySign, result.CurrencySign);
    }

    [Fact]
    public async Task Calculate_ReturnsExpectedPenaltyResult()
    {
        // Arrange
        var checkout = new DateTime(2022, 1, 1);
        var returnDate = new DateTime(2022, 1, 13);
        var countryId = 1;
        var expectedPenalty = 15.00m;
        var expectedBusinessDays = 13;
        var expectedCurrencySign = "$";

        _mockUnitOfRepository.Setup(x => x.Countries.GetCountry(countryId))
            .ReturnsAsync(new Country {CurrencySign = "$"});
        _mockUnitOfRepository.Setup(x => x.Holidays.GetCountryHoliday(countryId)).ReturnsAsync(new List<Holiday>());
        _mockUnitOfRepository.Setup(x => x.Weekends.GetCountryWeekend(countryId)).ReturnsAsync(new List<Weekend>());

        // Act
        var result = await _bookService.Calculate(checkout, returnDate, countryId);

        // Assert
        Assert.Equal(expectedPenalty, result.Penalty);
        Assert.Equal(expectedBusinessDays, result.BusinessDays);
        Assert.Equal(expectedCurrencySign, result.CurrencySign);
    }

    [Fact]
    public async Task Calculate_ReturnsExpectedWithHolidayResult()
    {
        // Arrange
        var checkout = new DateTime(2022, 1, 1);
        var returnDate = new DateTime(2022, 1, 13);
        var countryId = 1;
        var expectedPenalty = 5.00m;
        var expectedBusinessDays = 11;
        var expectedCurrencySign = "$";
        List<Holiday> holidays = new List<Holiday>()
        {
            new Holiday()
            {
                Id = 1, Country = new Country() {Id = 1, CurrencySign = "$", Name = "Iran"}, CountryId = 1,
                Date = new DateTime(2022, 1, 10), Event = "Holiday"
            },
            new Holiday()
            {
                Id = 2, Country = new Country() {Id = 1, CurrencySign = "$", Name = "Iran"}, CountryId = 1,
                Date = new DateTime(2022, 1, 11), Event = "Holiday"
            }
        };

        _mockUnitOfRepository.Setup(x => x.Countries.GetCountry(countryId))
            .ReturnsAsync(new Country {CurrencySign = "$"});
        _mockUnitOfRepository.Setup(x => x.Holidays.GetCountryHoliday(countryId)).ReturnsAsync(holidays);
        _mockUnitOfRepository.Setup(x => x.Weekends.GetCountryWeekend(countryId)).ReturnsAsync(new List<Weekend>());

        // Act
        var result = await _bookService.Calculate(checkout, returnDate, countryId);

        // Assert
        Assert.Equal(expectedPenalty, result.Penalty);
        Assert.Equal(expectedBusinessDays, result.BusinessDays);
        Assert.Equal(expectedCurrencySign, result.CurrencySign);
    }
    
    [Fact]
    public async Task Calculate_ReturnsExpectedWithWeekendsResult()
    {
        // Arrange
        var checkout = new DateTime(2022, 1, 1);
        var returnDate = new DateTime(2022, 1, 13);
        var countryId = 1;
        var expectedPenalty = 10.00m;
        var expectedBusinessDays = 12;
        var expectedCurrencySign = "$";
        List<Weekend> weekends = new List<Weekend>()
        {
            new Weekend()
            {
                Id = 1, Country = new Country() {Id = 1, CurrencySign = "$", Name = "Iran"}, CountryId = 1,
                Day = "Friday"
            }
        };

        _mockUnitOfRepository.Setup(x => x.Countries.GetCountry(countryId))
            .ReturnsAsync(new Country {CurrencySign = "$"});
        _mockUnitOfRepository.Setup(x => x.Holidays.GetCountryHoliday(countryId)).ReturnsAsync(new List<Holiday>());
        _mockUnitOfRepository.Setup(x => x.Weekends.GetCountryWeekend(countryId)).ReturnsAsync(weekends);

        // Act
        var result = await _bookService.Calculate(checkout, returnDate, countryId);

        // Assert
        Assert.Equal(expectedPenalty, result.Penalty);
        Assert.Equal(expectedBusinessDays, result.BusinessDays);
        Assert.Equal(expectedCurrencySign, result.CurrencySign);
    }

    [Fact]
    public async Task Calculate_ThrowsException_WhenCheckoutDateIsGreaterThanReturnDate()
    {
        // Arrange
        var checkout = new DateTime(2022, 1, 10);
        var returnDate = new DateTime(2022, 1, 1);
        var countryId = 1;

        _mockUnitOfRepository.Setup(x => x.Countries.GetCountry(countryId))
            .ReturnsAsync(new Country {CurrencySign = "$"});
        _mockUnitOfRepository.Setup(x => x.Holidays.GetCountryHoliday(countryId)).ReturnsAsync(new List<Holiday>());
        _mockUnitOfRepository.Setup(x => x.Weekends.GetCountryWeekend(countryId)).ReturnsAsync(new List<Weekend>());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _bookService.Calculate(checkout, returnDate, countryId));
    }
}