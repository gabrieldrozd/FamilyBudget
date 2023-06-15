using FamilyBudget.Api.Controllers.Base;
using FamilyBudget.Application.Features.Auth.Queries;
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

        // TODO: Create some result model
        // TODO: Create some result model
        // TODO: Create some result model
        // return BuildEnvelope(result);

        // TODO:
        // - Add some result mapper (DataEnvelope alike)
        // - Login endpoint
        // - Register endpoint

        // THEN:
        // - CreateBudgetPlan command and endpoint
        // - AddIncome
        // - AddExpense
        // - ShareBudgetPlan
        // - ... and other similar, which can help with UI development

        // Additionally:
        // - Create Browse and Filter endpoints for BudgetPlans, Users
        // ---- BrowseUsers will be used to list the users available to share with
        // ---- BrowseBudgetPlans will be used to list the budget plans
        // ---- FilterBudgetPlans will be used to filter the budget plans by name, date, etc.
        // - Create some endpoints for the BudgetPlanDetails

        return Ok(result);
    }
}