using System.Linq.Expressions;
using FamilyBudget.Application.Features.Finances.Budget.Commands;
using FamilyBudget.Application.Features.Finances.Budget.Commands.Handlers;
using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Common.Results;
using FamilyBudget.Common.Results.Core;
using FamilyBudget.Domain.Definitions;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Interfaces.Auth;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Repositories.Base;
using FluentAssertions;
using Moq;

namespace FamilyBudget.UnitTests.Features.Finances.Budget;

public class CreateBudgetPlanHandlerTests
{
    private readonly Mock<IBudgetPlanRepository> _budgetPlanRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CreateBudgetPlanHandlerTests()
    {
        _budgetPlanRepositoryMock = new Mock<IBudgetPlanRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _userContextMock = new Mock<IUserContext>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task CreateBudgetPlan_Handle_ShouldReturnNotFound_WhenUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userContextMock.Setup(x => x.UserId).Returns(userId);
        _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync((User) null);

        var handler = new CreateBudgetPlanHandler(
            _budgetPlanRepositoryMock.Object,
            _userRepositoryMock.Object,
            _userContextMock.Object,
            _unitOfWorkMock.Object);

        var command = new CreateBudgetPlan(
            "Test Budget Plan",
            "Test Description",
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(30));

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Name.Should().BeEquivalentTo("NotFound");
    }

    [Fact]
    public async Task CreateBudgetPlan_Handle_ShouldReturnBudgetPlanId_WhenUserFoundAndCommitSuccessful()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userContextMock.Setup(x => x.UserId).Returns(userId);
        var user = User.Create(userId, new UserDefinition
        {
            Email = "test@email.com",
            FirstName = "Test",
            LastName = "User",
            Role = Role.Member.Name
        });

        _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(user);

        _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(Result.Success());

        var handler = new CreateBudgetPlanHandler(
            _budgetPlanRepositoryMock.Object,
            _userRepositoryMock.Object,
            _userContextMock.Object,
            _unitOfWorkMock.Object);

        var command = new CreateBudgetPlan(
            "Test Budget Plan",
            "Test Description",
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(30));

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task CreateBudgetPlan_Handle_ShouldReturnDatabaseFailure_WhenUserFoundAndCommitFails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userContextMock.Setup(x => x.UserId).Returns(userId);
        var user = User.Create(userId, new UserDefinition
        {
            Email = "test@email.com",
            FirstName = "Test",
            LastName = "User",
            Role = Role.Member.Name
        });

        _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(user);

        _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(Result.DatabaseFailure());

        var handler = new CreateBudgetPlanHandler(
            _budgetPlanRepositoryMock.Object,
            _userRepositoryMock.Object,
            _userContextMock.Object,
            _unitOfWorkMock.Object);

        var command = new CreateBudgetPlan(
            "Test Budget Plan",
            "Test Description",
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(30));

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Name.Should().BeEquivalentTo(Failure.Database.Name);
    }
}