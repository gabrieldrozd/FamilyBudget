using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Features.Users.Commands;
using FamilyBudget.Common.Extensions;
using FamilyBudget.Domain.Definitions;
using FamilyBudget.Domain.Entities;

namespace FamilyBudget.Application.Mappings;

public static class UserMappings
{
    public static UserDefinition ToDefinition(RegisterUser model)
        => new()
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Role = model.Role
        };

    public static UserDto ToDto(User model)
        => new()
        {
            ExternalId = model.ExternalId,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Role = model.Role.Name,
            BudgetPlans = model.BudgetPlans.MapTo(BugdetPlanMappings.ToDto),
            SharedBudgets = model.SharedBudgets.MapTo(SharedBudgetMappings.ToDto)
        };

    public static UserBaseDto ToBaseDto(User model)
        => new()
        {
            ExternalId = model.ExternalId,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Role = model.Role.Name,
            BudgetPlans = model.BudgetPlans.Count,
            SharedBudgets = model.SharedBudgets.Count
        };
}