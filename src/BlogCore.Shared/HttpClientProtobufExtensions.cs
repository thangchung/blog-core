using Google.Protobuf;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogCore.Shared
{
    public static class HttpClientProtobufExtensions
    {
        public static async Task<T> GetProtobufAsync<T>(this HttpClient httpClient, string requestUri)
            where T : IMessage<T>, new()
        {
            var stringContent = await httpClient.GetStringAsync(requestUri);
            return JsonConvert.DeserializeObject<T>(stringContent, new ProtoMessageConverter());
        }

        public static string SerializeProtobufToJson<T>(this T data) where T : IMessage<T>, new()
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
