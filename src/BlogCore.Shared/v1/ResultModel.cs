using Newtonsoft.Json;
using System.Net;

namespace BlogCore.Shared.v1
{
    public class ResultModel
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public string Message { get; set; } = "Result Model.";
        public object Data { get; }
        public ResultModel(object data)
        {
            Data = JsonConvert.DeserializeObject(data.ToString());
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
