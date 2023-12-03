namespace Radha.Dtos;

public class BookCheckDto
{
    public BookCheckDto()
    {
    }

    public decimal Penalty { get; set; }
    public int BusinessDays { get; set; }
    public string CurrencySign { get; set; }
}