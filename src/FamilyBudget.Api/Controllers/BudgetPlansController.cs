using FamilyBudget.Api.Controllers.Base;
using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Features.Finances.Budget.Commands;
using FamilyBudget.Application.Features.Finances.Budget.Queries;
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
    [HttpGet("{budgetPlanId:guid}")]
    public async Task<IActionResult> Details(
        [FromRoute] Guid budgetPlanId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetBudgetPlanDetails(budgetPlanId);
        var result = await Sender.Send(query, cancellationToken);
        return BuildEnvelope(result);
    }

    [HttpPut("browse/{userExternalId:guid}")]
    public async Task<IActionResult> Browse(
        [FromRoute] Guid userExternalId,
        [FromBody] PaginationRequest pagination,
        CancellationToken cancellationToken = default)
    {
        var query = new BrowseUserBudgetPlans(userExternalId, pagination.ToPagination());
        var result = await Sender.Send(query, cancellationToken);
        return BuildEnvelope(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Register([FromBody] CreateBudgetPlan command, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(command, cancellationToken);
        return BuildEnvelope(result);
    }

    [HttpDelete("{budgetPlanId:guid}/delete")]
    public async Task<IActionResult> Delete([FromRoute] Guid budgetPlanId, CancellationToken cancellationToken = default)
    {
        var command = new DeleteBudgetPlan(budgetPlanId);
        var result = await Sender.Send(command, cancellationToken);
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

    [HttpDelete("{budgetPlanId:guid}/expense/{expenseId:guid}/delete")]
    public async Task<IActionResult> DeleteExpense(
        [FromRoute] Guid budgetPlanId,
        [FromRoute] Guid expenseId,
        CancellationToken cancellationToken = default)
    {
        var command = new RemoveExpense(budgetPlanId, expenseId);
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

    [HttpDelete("{budgetPlanId:guid}/income/{incomeId:guid}/delete")]
    public async Task<IActionResult> DeleteIncome(
        [FromRoute] Guid budgetPlanId,
        [FromRoute] Guid incomeId,
        CancellationToken cancellationToken = default)
    {
        var command = new RemoveIncome(budgetPlanId, incomeId);
        var result = await Sender.Send(command, cancellationToken);
        return BuildEnvelope(result);
    }
}