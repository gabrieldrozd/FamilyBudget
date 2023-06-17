using FamilyBudget.Api.Controllers.Base;
using FamilyBudget.Application.Features.Finances.BudgetPlans.Commands;
using FamilyBudget.Application.Features.Finances.BudgetPlans.Queries;
using FamilyBudget.Application.Features.Finances.MoneyFlow.Commands;
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

    [HttpPost("{budgetPlanId:guid}/income/add")]
    public async Task<IActionResult> AddIncome(
        [FromRoute] Guid budgetPlanId,
        [FromBody] AddIncome command,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(command with { BudgetPlanId = budgetPlanId }, cancellationToken);
        return BuildEnvelope(result);
    }

    [HttpPost("{budgetPlanId:guid}/expense/add")]
    public async Task<IActionResult> AddExpense(
        [FromRoute] Guid budgetPlanId,
        [FromBody] AddExpense command,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(command with { BudgetPlanId = budgetPlanId }, cancellationToken);
        return BuildEnvelope(result);
    }
}