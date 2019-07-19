using Google.Protobuf;
using Newtonsoft.Json;
using System.Net;

namespace BlogCore.Shared.v1
{
    public abstract class ResultModelBase
    {
        protected int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        protected string Message { get; set; } = "Result Model.";

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class JsonResultModel<TData> : ResultModelBase
    {
        public TData Data { get; set; }

        public JsonResultModel(TData data)
        {
            Data = data;
        }
    }

    public class ProtoResultModel<TData> : ResultModelBase 
        where TData : IMessage<TData>, new()
    {
        public TData Data { get; set; }

        public ProtoResultModel(TData data)
        {
            Data = data;
        }
    }
}
