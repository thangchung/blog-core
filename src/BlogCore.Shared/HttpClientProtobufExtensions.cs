using BlogCore.Shared.v1;
using BlogCore.Shared.v1.Common;
using Google.Protobuf;
using Newtonsoft.Json;
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
        public static async Task<ProtoResultModel<TProto>> GetProtoAsync<TProto>(this HttpClient httpClient, string requestUri)
            where TProto : IMessage<TProto>, new()
        {
            var stringContent = await httpClient.GetStringAsync(requestUri);
            var resultModel = JsonConvert.DeserializeObject<ProtoResultModel<TProto>>(stringContent);
            return resultModel;
        }

        public static Task<ProtoResultModel<TProto>> PostProtoAsync<TProto>(this HttpClient httpClient, string requestUri, object content)
            where TProto : IMessage<TProto>, new()
            => httpClient.SendProtoAsync<TProto>(HttpMethod.Post, requestUri, content);

        public static Task<ProtoResultModel<TProto>> PutProtoAsync<TProto>(this HttpClient httpClient, string requestUri, object content)
            where TProto : IMessage<TProto>, new()
            => httpClient.SendProtoAsync<TProto>(HttpMethod.Put, requestUri, content);

        public static async Task<ProtoResultModel<TProto>> SendProtoAsync<TProto>(this HttpClient httpClient, HttpMethod method, string requestUri, object content)
            where TProto : IMessage<TProto>, new()
        {
            var result = JsonConvert.SerializeObject(content);
            var response = await httpClient.SendAsync(new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json")
            });

            response.EnsureSuccessStatusCode();

            if (typeof(ProtoResultModel<TProto>) == typeof(IgnoreResponse))
            {
                return default;
            }
            else
            {
                var resultModel = JsonConvert.DeserializeObject<ProtoResultModel<TProto>>(content.ToString());
                return resultModel;
            }
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
}
