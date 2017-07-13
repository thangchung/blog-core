using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.Core
{
    public interface IObjectOutputBoundary<in TInput, TOutput>
        where TInput : IMesssage
        where TOutput : IViewModel
    {
        Task<TOutput> TransformAsync(TInput input);
    }

    public interface IEnumerableOutputBoundary<in TInput, TOutput>
        where TInput : IEnumerable<IMesssage>
    {
        Task<TOutput> TransformAsync(TInput input);
    }
}