using MediatR;
using Shared;

namespace Application.Abstractions;

public interface IQueryHandler<in TQuery,TResponse> : IRequestHandler<TQuery,Result<TResponse>> 
    where TQuery : IQuery<TResponse>
{
}
