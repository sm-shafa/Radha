using Backend_Assessment.Models;

namespace Radha.Entities;

public class Country
{
    public int Id { set; get; }
    public string Name { set; get; }
    public string CurrencySign { set; get; }
    public List<Holiday> Holidays { set; get; }
    public List<Weekend> Weekends { set; get; }
}