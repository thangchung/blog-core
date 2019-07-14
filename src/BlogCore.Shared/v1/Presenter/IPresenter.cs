using Google.Protobuf;

namespace BlogCore.Shared.v1.Presenter
{
    public interface IApiPresenter<TData> where TData : IMessage<TData>, new()
    {
        dynamic Handle(TData resultModel);
    }
}
