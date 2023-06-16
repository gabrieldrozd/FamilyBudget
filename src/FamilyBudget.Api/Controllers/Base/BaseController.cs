using FamilyBudget.Common.Api.Envelope;
using FamilyBudget.Common.Results;
using FamilyBudget.Common.Results.Core;
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

    protected IActionResult BuildEnvelope(Result result)
    {
        var statusCode = GetStatusCode(result);
        var envelope = ToEnvelope(result);

        return StatusCode(statusCode, envelope.WithCode(statusCode));
    }

    protected IActionResult BuildEnvelope<T>(Result<T> result)
    {
        var statusCode = GetStatusCode(result);
        var envelope = ToEnvelope(result);

        return StatusCode(statusCode, envelope.WithCode(statusCode));
    }

    private static EmptyEnvelope ToEnvelope(Result result)
    {
        return result.Status switch
        {
            Status.Success when result.IsSuccess => new EmptyEnvelope(),
            Status.Failure when !result.IsSuccess => new EmptyEnvelope(result.Error),
            Status.NotFound when !result.IsSuccess => new EmptyEnvelope(result.Error),
            Status.Unauthorized when !result.IsSuccess => new EmptyEnvelope(result.Error),
            _ => new EmptyEnvelope(Error.Unexpected())
        };
    }

    private static Envelope<T> ToEnvelope<T>(Result<T> result)
    {
        return result.Status switch
        {
            Status.Success when result.IsSuccess => new Envelope<T>(result.Value),
            Status.Failure when !result.IsSuccess => new Envelope<T>(result.Error),
            Status.NotFound when !result.IsSuccess => new Envelope<T>(result.Error),
            Status.Unauthorized when !result.IsSuccess => new Envelope<T>(result.Error),
            _ => new Envelope<T>(Error.Unexpected())
        };
    }

    private static int GetStatusCode(Result result)
    {
        return result.Status switch
        {
            Status.Success when result.IsSuccess => StatusCodes.Status200OK,
            Status.Failure when !result.IsSuccess => StatusCodes.Status400BadRequest,
            Status.NotFound when !result.IsSuccess => StatusCodes.Status404NotFound,
            Status.Unauthorized when !result.IsSuccess => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static int GetStatusCode<T>(Result<T> result)
    {
        return GetStatusCode((Result) result);
    }
}