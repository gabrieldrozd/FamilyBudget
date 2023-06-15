using FamilyBudget.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Api.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private ISender _sender;

    protected ISender Sender => _sender ??= HttpContext.RequestServices
        .GetService<ISender>();

    // TODO: Adjust those, as they should be mapped in a Envelope fashion way to the response
    protected IActionResult HandleResult(Result result)
    {
        if (result == null)
            return NotFound();
        if (result.IsSuccess)
            return Ok();
        return BadRequest(result.Error);
    }

    // TODO: Adjust those, as they should be mapped in a Envelope fashion way to the response
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result is null)
            return NotFound();
        if (result.IsSuccess && result.Value != null)
            return Ok(result.Value);
        if (result.IsSuccess && result.Value == null)
            return NotFound();
        return BadRequest(result.Error);
    }
}