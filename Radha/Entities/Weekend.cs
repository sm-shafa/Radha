using Radha.Entities;

namespace Backend_Assessment.Models;

public class Weekend
{
    public int Id { get; set; }
    public string Day { get; set; }
    public int CountryId { set; get; }
    public Country Country { get; set; }
}