using Radha.Entities;

namespace Backend_Assessment.Models;

public class Holiday
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Event { get; set; }
    public int CountryId { set; get; }
    public Country Country { get; set; }
}