using FamilyBudget.Api.Controllers.Base;
using FamilyBudget.Application.Features.BudgetPlans.Commands;
using FamilyBudget.Application.Features.BudgetPlans.Queries;
using FamilyBudget.Common.Api.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Api.Controllers;

[Tags(ApiSettings.BudgetPlans)]
[Area(ApiSettings.BudgetPlans)]
[ApiExplorerSettings(GroupName = ApiGroups.Financial)]
[Route("api/budget-plans")]
public class BudgetPlansController : BaseController
{
    [HttpPut("browse")]
    public async Task<IActionResult> Browse([FromBody] PaginationRequest pagination, CancellationToken cancellationToken = default)
    {
        var query = new BrowseBudgetPlans(pagination.ToPagination());
        var result = await Sender.Send(query, cancellationToken);
        return BuildEnvelope(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Register([FromBody] CreateBudgetPlan command, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(command, cancellationToken);
        return BuildEnvelope(result);
    }
}