using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Features.Finances.Budget.Queries;
using FamilyBudget.Common.Api.Pagination;
using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Domain.Definitions;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Infrastructure.Queries.BudgetPlans;
using FluentAssertions;
using Moq;

namespace FamilyBudget.UnitTests.Features.Finances.Budget;

public class BrowseBudgetsSharedWithUserHandlerTests
{
    private readonly Mock<ISharedBudgetRepository> _sharedBudgetRepositoryMock;

    public BrowseBudgetsSharedWithUserHandlerTests()
    {
        _sharedBudgetRepositoryMock = new Mock<ISharedBudgetRepository>();
    }

    [Fact]
    public async Task BrowseBudgetsSharedWithUser_Handle_ShouldReturnSuccess_WhenBudgetPlansFound()
    {
        // Arrange
        var userExternalId = Guid.NewGuid();
        var sharedBudgets = new List<SharedBudget>
        {
            SharedBudget.Create(Guid.NewGuid(), Guid.NewGuid(), userExternalId, DateTime.Now),
            SharedBudget.Create(Guid.NewGuid(), Guid.NewGuid(), userExternalId, DateTime.Now),
        };

        foreach (var budget in sharedBudgets)
        {
            budget.User = User.Create(Guid.NewGuid(), new UserDefinition
            {
                Email = "",
                FirstName = "",
                LastName = "",
                Role = Role.Member.Name
            });

            budget.BudgetPlan = BudgetPlan.Create(Guid.NewGuid(), new BudgetPlanDefinition
            {
                Name = "",
                Description = "",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
            }, Guid.NewGuid());
        }



        var pagination = new Pagination(1, 10, true);
        var paginatedList = PaginatedList<SharedBudget>.Create(pagination, sharedBudgets, sharedBudgets.Count);
        _sharedBudgetRepositoryMock.Setup(x => x.BrowseBudgetsSharedWithUserAsync(userExternalId, pagination))
            .ReturnsAsync(paginatedList);

        var handler = new BrowseBudgetsSharedWithUserHandler(_sharedBudgetRepositoryMock.Object);

        var query = new BrowseBudgetsSharedWithUser(userExternalId, pagination);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.List.Should().HaveCount(2);
    }

    [Fact]
    public async Task BrowseBudgetsSharedWithUser_Handle_ShouldReturnSuccess_WithEmptyList_WhenNoBudgetPlansFound()
    {
        // Arrange
        var userExternalId = Guid.NewGuid();
        var sharedBudgets = new List<SharedBudget>();

        var pagination = new Pagination(1, 10, true);
        var paginatedList = PaginatedList<SharedBudget>.Create(pagination, sharedBudgets, sharedBudgets.Count);
        _sharedBudgetRepositoryMock.Setup(x => x.BrowseBudgetsSharedWithUserAsync(userExternalId, pagination))
            .ReturnsAsync(paginatedList);

        var handler = new BrowseBudgetsSharedWithUserHandler(_sharedBudgetRepositoryMock.Object);

        var query = new BrowseBudgetsSharedWithUser(userExternalId, pagination);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.List.Should().BeEmpty();
    }
}