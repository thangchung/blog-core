using BlogCore.Shared.v1.Blog;
using Google.Protobuf;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlogCore.Shared.v1;

namespace BlogCore.Shared
{
    public static class HttpClientProtobufExtensions
    {
        public static async Task<TResultModel> GetProtobufAsync<TResultModel, TProtoModel>(this HttpClient httpClient, string requestUri)
            where TResultModel : ProtoResultModel<TProtoModel>
            where TProtoModel : IMessage<TProtoModel>, new()
        {
            var stringContent = await httpClient.GetStringAsync(requestUri);
            var resultModel = JsonConvert.DeserializeObject<TResultModel>(stringContent);
            resultModel.Data = JsonConvert.DeserializeObject<TProtoModel>(resultModel.Data.ToString(), new ProtoMessageConverter());
            return resultModel;
        }

        public static string SerializeProtobufToJson<TProtoModel>(this TProtoModel data) 
            where TProtoModel : IMessage<TProtoModel>, new()
        {
            return JsonConvert.SerializeObject(data, new ProtoMessageConverter());
        }
    }

    /// <summary>
    /// Ref to https://github.com/GoogleCloudPlatform/dotnet-docs-samples/blob/8a942cae26/monitoring/api/AlertSample/Program.cs
    /// </summary>
    public class ProtoMessageConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IMessage).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Read an entire object from the reader.
            var converter = new ExpandoObjectConverter();
            object o = converter.ReadJson(reader, objectType, existingValue, serializer);

            // Convert it back to json text.
            string text = JsonConvert.SerializeObject(o);

            // And let protobuf's parser parse the text.
            IMessage message = (IMessage)Activator.CreateInstance(objectType);
            return JsonParser.Default.Parse(text, message.Descriptor);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(JsonFormatter.Default.Format((IMessage)value));
        }
    }
}
