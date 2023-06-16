using FamilyBudget.Api.Controllers.Base;
using FamilyBudget.Application.Features.Users.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Api.Controllers;

[Tags(ApiSettings.Users)]
[Area(ApiSettings.Users)]
[ApiExplorerSettings(GroupName = ApiGroups.Users)]
public class UsersController : BaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUser command, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(command, cancellationToken);
        return BuildEnvelope(result);
    }
}