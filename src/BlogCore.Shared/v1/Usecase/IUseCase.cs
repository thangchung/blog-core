using Google.Protobuf;
using System.Threading.Tasks;

namespace BlogCore.Shared.v1.Usecase
{
    public interface IUseCase<TRequest, TResponse> 
        where TRequest : class, IMessage 
        where TResponse : class, IMessage
    {
        Task<TResponse> HandleAsync(TRequest request) ;
    }
}
