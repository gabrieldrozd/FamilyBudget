using System.Linq.Expressions;
using FamilyBudget.Application.Features.Auth.Commands;
using FamilyBudget.Application.Features.Auth.Commands.Handlers;
using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Common.Models;
using FamilyBudget.Common.Results;
using FamilyBudget.Common.Results.Core;
using FamilyBudget.Domain.Definitions;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Domain.Interfaces.Services;
using FluentAssertions;
using Moq;

namespace FamilyBudget.UnitTests.Features.Auth;

public class LoginUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IIdentityService> _identityServiceMock;

    public LoginUserHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _identityServiceMock = new Mock<IIdentityService>();
    }

    [Fact]
    public async Task LoginUser_Handle_ShouldReturnFailure_WhenUserNotFoundOrInvalidPassword()
    {
        // Arrange
        var email = "test@email.com";
        var password = "password";

        _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync((User) null);

        var handler = new LoginUserHandler(
            _userRepositoryMock.Object,
            _identityServiceMock.Object);

        var command = new LoginUser(email, password);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Name.Should().BeEquivalentTo(Failure.InvalidCredentials.Name);
    }

    [Fact]
    public async Task LoginUser_Handle_ShouldReturnToken_WhenUserFoundAndValidPassword()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var email = "test@email.com";
        var password = "password";

        var definition = new UserDefinition
        {
            Email = email,
            FirstName = "Test",
            LastName = "User",
            Role = Role.Member.Name
        };

        var user = User.Create(userId, definition);

        _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(user);

        var token = new AccessToken
        {
            Token = "Test",
            UserId = userId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role.Name
        };

        _identityServiceMock.Setup(x => x.Login(user, password)).Returns(Result.Success(token));

        var handler = new LoginUserHandler(
            _userRepositoryMock.Object,
            _identityServiceMock.Object);

        var command = new LoginUser(email, password);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(token);
    }
}