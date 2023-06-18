using System.Linq.Expressions;
using FamilyBudget.Application.Features.Auth.Queries;
using FamilyBudget.Common.Domain.ValueObjects;
using FamilyBudget.Common.Models;
using FamilyBudget.Domain.Definitions;
using FamilyBudget.Domain.Entities;
using FamilyBudget.Domain.Interfaces.Auth;
using FamilyBudget.Domain.Interfaces.Providers;
using FamilyBudget.Domain.Interfaces.Repositories;
using FamilyBudget.Infrastructure.Queries.Auth;
using FluentAssertions;
using Moq;

namespace FamilyBudget.UnitTests.Features.Auth;

public class GetCurrentUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ITokenProvider> _tokenProviderMock;
    private readonly Mock<IUserContext> _userContextMock;

    public GetCurrentUserHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _tokenProviderMock = new Mock<ITokenProvider>();
        _userContextMock = new Mock<IUserContext>();
    }

    [Fact]
    public async Task GetCurrentUser_Handle_ShouldReturnUnauthorized_WhenUserNotFound()
    {
        // Arrange
        _userContextMock.Setup(x => x.UserId).Returns(Guid.NewGuid());
        _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync((User) null);

        var handler = new GetCurrentUserHandler(
            _userRepositoryMock.Object,
            _tokenProviderMock.Object,
            _userContextMock.Object);

        var query = new GetCurrentUser();

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Name.Should().BeEquivalentTo("Unauthorized");
    }

    [Fact]
    public async Task GetCurrentUser_Handle_ShouldReturnToken_WhenUserFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userContextMock.Setup(x => x.UserId).Returns(userId);

        var definition = new UserDefinition
        {
            Email = "test@email.com",
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

        _tokenProviderMock.Setup(x => x.CreateToken(
                It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Role>()))
            .Returns(token);

        var handler = new GetCurrentUserHandler(
            _userRepositoryMock.Object,
            _tokenProviderMock.Object,
            _userContextMock.Object);

        var query = new GetCurrentUser();

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(token);
    }
}