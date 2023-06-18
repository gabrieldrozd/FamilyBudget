using FamilyBudget.Api.Controllers.Base;
using FamilyBudget.Application.Features.Users.Commands;
using FamilyBudget.Application.Features.Users.Queries;
using FamilyBudget.Common.Api.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Api.Controllers;

[Tags(ApiSettings.Users)]
[Area(ApiSettings.Users)]
[ApiExplorerSettings(GroupName = ApiGroups.Users)]
[Route("api/users")]
public class UsersController : BaseController
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var query = new GetAllUsersExceptCurrent();
        var result = await Sender.Send(query, cancellationToken);
        return BuildEnvelope(result);
    }

    [HttpPut("browse")]
    public async Task<IActionResult> Browse([FromBody] PaginationRequest pagination, CancellationToken cancellationToken = default)
    {
        var query = new BrowseUsers(pagination.ToPagination());
        var result = await Sender.Send(query, cancellationToken);
        return BuildEnvelope(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUser command, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(command, cancellationToken);
        return BuildEnvelope(result);
    }

    [HttpDelete("{externalId:guid}/delete")]
    public async Task<IActionResult> Delete(Guid externalId, CancellationToken cancellationToken = default)
    {
        var command = new DeleteUser(externalId);
        var result = await Sender.Send(command, cancellationToken);
        return BuildEnvelope(result);
    }
}