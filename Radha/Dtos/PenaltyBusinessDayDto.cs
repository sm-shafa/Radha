namespace Radha.Dtos;

public class PenaltyBusinessDayDto
{
    public PenaltyBusinessDayDto()
    {
    }

    public decimal Penalty { get; set; }
    public int BusinessDays { get; set; }
    public string CurrencySign { get; set; }
}