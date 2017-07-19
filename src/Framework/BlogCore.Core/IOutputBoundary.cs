using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.Core
{
    public interface IObjectOutputBoundary<in TInput, TOutput>
        where TInput : IMessage
        where TOutput : IViewModel
    {
        Task<TOutput> TransformAsync(TInput input);
    }

    public interface IEnumerableOutputBoundary<in TInput, TOutput>
        where TInput : IEnumerable<IMessage>
    {
        Task<TOutput> TransformAsync(TInput input);
    }
}