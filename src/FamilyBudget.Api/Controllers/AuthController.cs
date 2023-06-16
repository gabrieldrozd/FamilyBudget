using FamilyBudget.Api.Controllers.Base;
using FamilyBudget.Application.Features.Auth.Commands;
using FamilyBudget.Application.Features.Auth.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Api.Controllers;

[Tags(ApiSettings.Auth)]
[Area(ApiSettings.Auth)]
[ApiExplorerSettings(GroupName = ApiGroups.Users)]
public class AuthController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken = default)
    {
        var query = new GetCurrentUser();
        var result = await Sender.Send(query, cancellationToken);
        return BuildEnvelope(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUser command, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(command, cancellationToken);
        return BuildEnvelope(result);
    }
}