namespace Radha.Models;

public class GetPenaltyBusinessDayModel
{
    public DateTime CheckedOutDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int CountryId { get; set; }
}