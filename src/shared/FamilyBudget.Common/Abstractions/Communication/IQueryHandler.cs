using FamilyBudget.Common.Results;
using MediatR;

namespace FamilyBudget.Common.Abstractions.Communication;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}