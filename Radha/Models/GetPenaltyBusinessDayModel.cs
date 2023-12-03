namespace Radha.Models;

public class GetPenaltyBusinessDayModel
{
    public DateTime DateCheckedOut { get; set; }
    public DateTime DateCheckedIn { get; set; }
    public int CountryId { get; set; }
}