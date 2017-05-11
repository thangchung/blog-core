using System.Collections.Generic;

namespace BlogCore.Core
{
    public interface IObjectOutputBoundary<in TInput, out TOutput>
        where TInput : IMesssage
        where TOutput : IViewModel
    {
        TOutput Transform(TInput input);
    }

    public interface IEnumerableOutputBoundary<in TInput, out TOutput>
        where TInput : IEnumerable<IMesssage>
        where TOutput : IEnumerable<IViewModel>
    {
        TOutput Transform(TInput input);
    }
}