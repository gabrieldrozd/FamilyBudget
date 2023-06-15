using FamilyBudget.Common.Results;
using MediatR;

namespace FamilyBudget.Common.Abstractions.Communication;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}