using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radha.Dtos;
using Radha.Models;

namespace Radha.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    public BookController()
    {
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("GetCalculationBook")]
    public async Task<ActionResult<PenaltyBusinessDayDto>> GetCalculationBook(GetPenaltyBusinessDayModel request,
        CancellationToken cancellationToken)
    {
        // var result = await _mediator.Send(request, cancellationToken);
        //
        // return Ok(result);
        // return Ok(result);
    }
}