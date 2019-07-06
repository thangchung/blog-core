using Google.Protobuf;
using Newtonsoft.Json;
using System.Net;

namespace BlogCore.Shared.v1
{
    public class ProtoResultModel<TData> where TData : IMessage<TData>, new()
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public string Message { get; set; } = "Result Model.";
        public TData Data { get; set; }

        public ProtoResultModel(TData data)
        {
            Data = JsonConvert.DeserializeObject<TData>(data.SerializeProtobufToJson());
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
