using BlogCore.Core;
using System;
using System.Threading.Tasks;

namespace BlogCore.Infrastructure.UseCase
{
    public interface IUseCaseRequestHandler<in TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        IObservable<TResponse> Process(TRequest request);
    }

    public interface IUseCaseRequestHandlerAsync<in TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Process(TRequest request);
    }

    public interface IAsyncUseCaseRequestHandler<in TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Process(TRequest request);
    }

    public interface IRequest<TResponse> : IMessage
    {

    }
}
