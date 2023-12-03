using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radha.Dtos;
using Radha.Models;
using Radha.Services;

namespace Radha.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    
    public BookController(IBookService bookService)
    {
        this._bookService = bookService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("GetCalculationBook")]
    public async Task<ActionResult<BookCheckDto>> GetCalculationBook(GetPenaltyBusinessDayModel request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _bookService.Calculate(request.CheckedOutDate, request.CheckedInDate, request.CountryId);
            return result;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}