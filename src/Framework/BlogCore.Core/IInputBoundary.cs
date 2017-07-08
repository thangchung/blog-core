using MediatR;

namespace BlogCore.Core
{
    public interface IInputBoundary<in TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
    }
}