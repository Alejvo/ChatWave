using MediatR;
using Shared;

namespace Application.Abstractions
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
