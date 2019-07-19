using Google.Protobuf;
using Newtonsoft.Json;
using System.Net;

namespace BlogCore.Shared.v1
{
    public class JsonResultModel<TData>
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public string Message { get; set; } = "Result Model.";
        public TData Data { get; set; }

        public JsonResultModel(TData data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ProtoResultModel<TData> where TData : IMessage<TData>, new()
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public string Message { get; set; } = "Result Model.";
        public TData Data { get; set; }

        public ProtoResultModel(TData data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
