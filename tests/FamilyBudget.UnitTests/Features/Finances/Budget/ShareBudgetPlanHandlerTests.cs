using System.Linq.Expressions;
using FamilyBudget.Application.Features.Finances.Budget.Commands;
using FamilyBudget.Application.Features.Finances.Budget.Commands.Handlers;
using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Common.Results;
using FamilyBudget.Domain.Definitions;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Entities.Budget;
using FamilyBudget.Domain.Interfaces.Auth;
using FamilyBudget.Domain.Interfaces.Providers;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;
using FluentAssertions;
using Moq;

namespace FamilyBudget.UnitTests.Features.Finances.Budget;

public class ShareBudgetPlanHandlerTests
{
    private readonly Mock<IBudgetPlanRepository> _budgetPlanRepositoryMock;
    private readonly Mock<ISharedBudgetRepository> _sharedBudgetRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IClock> _clockMock;

    public ShareBudgetPlanHandlerTests()
    {
        _budgetPlanRepositoryMock = new Mock<IBudgetPlanRepository>();
        _sharedBudgetRepositoryMock = new Mock<ISharedBudgetRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _userContextMock = new Mock<IUserContext>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _clockMock = new Mock<IClock>();
    }

    [Fact]
    public async Task ShareBudgetPlan_Handle_ShouldReturnSuccess_WhenSharingProcessIsSuccessful()
    {
        // Arrange
        var budgetPlanId = Guid.NewGuid();
        var userId = Guid.NewGuid();
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
        var currentUser = User.Create(userId, new UserDefinition
        {
            Email = "test@email.com",
            FirstName = "Test",
            LastName = "User",
            Role = Role.Member.Name
        });
        var usersToShareWith = new List<User> { currentUser };

        _budgetPlanRepositoryMock.Setup(x => x.GetByIdAsync(budgetPlanId)).ReturnsAsync(budgetPlan);
        _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(currentUser);
        _userContextMock.Setup(x => x.UserId).Returns(userId);
        _userRepositoryMock.Setup(x => x.GetByIds(It.IsAny<Guid[]>())).ReturnsAsync(usersToShareWith);
        _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(Result.Success());
        _clockMock.Setup(x => x.Current()).Returns(DateTime.UtcNow);

        var handler = new ShareBudgetPlanHandler(
            _budgetPlanRepositoryMock.Object,
            _sharedBudgetRepositoryMock.Object,
            _userRepositoryMock.Object,
            _userContextMock.Object,
            _unitOfWorkMock.Object,
            _clockMock.Object);

        var command = new ShareBudgetPlan(budgetPlanId, new[] { userId });

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task ShareBudgetPlan_Handle_ShouldReturnNotFound_WhenBudgetPlanNotFound()
    {
        // Arrange
        var budgetPlanId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _budgetPlanRepositoryMock.Setup(x => x.GetByIdAsync(budgetPlanId)).ReturnsAsync((BudgetPlan) null);

        var handler = new ShareBudgetPlanHandler(
            _budgetPlanRepositoryMock.Object,
            _sharedBudgetRepositoryMock.Object,
            _userRepositoryMock.Object,
            _userContextMock.Object,
            _unitOfWorkMock.Object,
            _clockMock.Object);

        var command = new ShareBudgetPlan(budgetPlanId, new[] { userId });

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Name.Should().BeEquivalentTo("NotFound");
    }
}