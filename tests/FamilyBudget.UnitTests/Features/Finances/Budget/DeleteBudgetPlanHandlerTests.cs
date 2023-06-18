using FamilyBudget.Application.Features.Finances.Budget.Commands;
using FamilyBudget.Application.Features.Finances.Budget.Commands.Handlers;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Definitions;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;
using FluentAssertions;
using Moq;

namespace FamilyBudget.UnitTests.Features.Finances.Budget;

public class DeleteBudgetPlanHandlerTests
{
    private readonly Mock<IBudgetPlanRepository> _budgetPlanRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public DeleteBudgetPlanHandlerTests()
    {
        _budgetPlanRepositoryMock = new Mock<IBudgetPlanRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task DeleteBudgetPlan_Handle_ShouldReturnNotFound_WhenBudgetPlanNotFound()
    {
        // Arrange
        var budgetPlanId = Guid.NewGuid();
        _budgetPlanRepositoryMock.Setup(x => x.GetByIdAsync(budgetPlanId))
            .ReturnsAsync((BudgetPlan) null);

        var handler = new DeleteBudgetPlanHandler(
            _budgetPlanRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var command = new DeleteBudgetPlan(budgetPlanId);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Name.Should().BeEquivalentTo("NotFound");
    }

    [Fact]
    public async Task DeleteBudgetPlan_Handle_ShouldReturnSuccess_WhenBudgetPlanFoundAndCommitSuccessful()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var budgetPlanId = Guid.NewGuid();
        var budgetPlan = BudgetPlan.Create(
            budgetPlanId,
            new BudgetPlanDefinition
            {
                Name = "Test",
                Description = "Test",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
            },
            userId
        );
        _budgetPlanRepositoryMock.Setup(x => x.GetByIdAsync(budgetPlanId))
            .ReturnsAsync(budgetPlan);

        _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(Result.Success());

        var handler = new DeleteBudgetPlanHandler(
            _budgetPlanRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var command = new DeleteBudgetPlan(budgetPlanId);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteBudgetPlan_Handle_ShouldReturnDatabaseFailure_WhenBudgetPlanFoundAndCommitFails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var budgetPlanId = Guid.NewGuid();
        var budgetPlan = BudgetPlan.Create(
            budgetPlanId,
            new BudgetPlanDefinition
            {
                Name = "Test",
                Description = "Test",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
            },
            userId
        );
        _budgetPlanRepositoryMock.Setup(x => x.GetByIdAsync(budgetPlanId))
            .ReturnsAsync(budgetPlan);

        _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(Result.DatabaseFailure());

        var handler = new DeleteBudgetPlanHandler(
            _budgetPlanRepositoryMock.Object,
            _unitOfWorkMock.Object);

        var command = new DeleteBudgetPlan(budgetPlanId);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Name.Should().BeEquivalentTo("DatabaseFailure");
    }
}