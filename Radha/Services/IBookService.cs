using Radha.Dtos;

namespace Radha.Services;

public interface IBookService
{
     Task<BookCheckDto> Calculate(DateTime checkout, DateTime checkin, int countryId);
}