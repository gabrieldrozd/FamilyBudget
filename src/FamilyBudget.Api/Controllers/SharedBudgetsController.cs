using FamilyBudget.Api.Controllers.Base;
using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Features.Finances.Budget.Commands;
using FamilyBudget.Application.Features.Finances.Budget.Queries;
using FamilyBudget.Common.Api.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Api.Controllers;

[Tags(ApiSettings.SharedBudgets)]
[Area(ApiSettings.SharedBudgets)]
[ApiExplorerSettings(GroupName = ApiGroups.Financial)]
[Route("api/shared-budgets")]
public class SharedBudgetsController : BaseController
{
    [HttpGet("{sharedBudgetId:guid}")]
    [ProducesResponseType(typeof(SharedBudgetDetailsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Details(
        [FromRoute] Guid sharedBudgetId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetSharedBudgetDetails(sharedBudgetId);
        var result = await Sender.Send(query, cancellationToken);
        return BuildEnvelope(result);
    }

    [HttpPut("browse/{userExternalId:guid}")]
    [ProducesResponseType(typeof(PaginatedList<SharedBudgetDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Browse(
        [FromRoute] Guid userExternalId,
        [FromBody] PaginationRequest pagination,
        CancellationToken cancellationToken = default)
    {
        var query = new BrowseBudgetsSharedWithUser(userExternalId, pagination.ToPagination());
        var result = await Sender.Send(query, cancellationToken);
        return BuildEnvelope(result);
    }

    [HttpPost("{budgetPlanId:guid}/share")]
    public async Task<IActionResult> Share(
        [FromRoute] Guid budgetPlanId,
        [FromBody] ShareBudgetPlan command,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(command with { BudgetPlanId = budgetPlanId }, cancellationToken);
        return BuildEnvelope(result);
    }
}