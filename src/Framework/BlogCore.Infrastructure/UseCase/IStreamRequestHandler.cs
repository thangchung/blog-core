using System;

namespace BlogCore.Infrastructure.UseCase
{
    public interface IUseCaseRequestHandler<in TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        IObservable<TResponse> Process(TRequest request);
    }
}
