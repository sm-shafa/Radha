namespace Radha.Models;

public class GetPenaltyBusinessDayModel
{
    public DateTime CheckedOutDate { get; set; }
    public DateTime CheckedInDate { get; set; }
    public int CountryId { get; set; }
}