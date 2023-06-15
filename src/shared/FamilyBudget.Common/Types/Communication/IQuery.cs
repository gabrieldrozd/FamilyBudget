using FamilyBudget.Common.Results;
using MediatR;

namespace FamilyBudget.Common.Types.Communication;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}