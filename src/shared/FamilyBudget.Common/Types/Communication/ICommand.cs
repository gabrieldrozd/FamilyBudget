using FamilyBudget.Common.Results;
using MediatR;

namespace FamilyBudget.Common.Types.Communication;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}