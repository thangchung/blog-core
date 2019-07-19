using BlogCore.Shared.v1;
using BlogCore.Shared.v1.Common;
using Google.Protobuf;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Shared
{
    /// <summary>
    /// Ref https://github.com/aspnet/AspNetCore/blob/master/src/Components/Blazor/Http/src/HttpClientJsonExtensions.cs
    /// </summary>
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

        public static Task<TResultModel> PostProtobufAsync<TResultModel, TProtoResponse>(this HttpClient httpClient, string requestUri, object content)
            where TResultModel : ProtoResultModel<TProtoResponse>
            where TProtoResponse : IMessage<TProtoResponse>, new()
            => httpClient.SendJsonAsync<TResultModel, TProtoResponse>(HttpMethod.Post, requestUri, content);

        public static Task<TResultModel> PutJsonAsync<TResultModel, TProtoResponse>(this HttpClient httpClient, string requestUri, object content)
            where TResultModel : ProtoResultModel<TProtoResponse>
            where TProtoResponse : IMessage<TProtoResponse>, new()
            => httpClient.SendJsonAsync<TResultModel, TProtoResponse>(HttpMethod.Put, requestUri, content);

        public static async Task<TResultModel> SendJsonAsync<TResultModel, TProtoResponse>(this HttpClient httpClient, HttpMethod method, string requestUri, object content)
            where TResultModel : ProtoResultModel<TProtoResponse>
            where TProtoResponse : IMessage<TProtoResponse>, new()
        {
            var result = JsonConvert.SerializeObject(content, new ProtoMessageConverter());
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json")
            });

            response.EnsureSuccessStatusCode();

            if (typeof(TResultModel) == typeof(IgnoreResponse))
            {
                return default;
            }
            else
            {
                var resultModel = JsonConvert.DeserializeObject<TResultModel>(content.ToString());
                resultModel.Data = JsonConvert.DeserializeObject<TProtoResponse>(resultModel.Data.ToString(), new ProtoMessageConverter());
                return resultModel;
            }
        }

        public static string SerializeProtobufToJson<TProtoModel>(this TProtoModel data) 
            where TProtoModel : IMessage<TProtoModel>, new()
        {
            return JsonConvert.SerializeObject(data, new ProtoMessageConverter());
        }

        class IgnoreResponse { }
    }

    public static class ProtoJsonExtensions
    {
        public static Dictionary<string, string> SerializeObjectToDictionary(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dicObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return dicObject;
        }

        public static ItemContainer SerializeData<TData>(this TData obj)
        {
            var dicObject = obj.SerializeObjectToDictionary();
            var container = new ItemContainer();
            foreach (var attr in dicObject)
            {
                container.Item.Add(attr.Key, attr.Value);
            }
            return container;
        }

        public static TData DeserializeData<TData>(this ItemContainer itemContainer)
        {
            var json = JsonConvert.SerializeObject(itemContainer.Item);
            return JsonConvert.DeserializeObject<TData>(json);
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
            var o = converter.ReadJson(reader, objectType, existingValue, serializer);

            // Convert it back to json text.
            var text = JsonConvert.SerializeObject(o);

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
