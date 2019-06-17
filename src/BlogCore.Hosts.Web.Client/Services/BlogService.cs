using BlogCore.Shared;
using BlogCore.Shared.v1.Blog;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogCore.Hosts.Web.Client.Services
{
    public class BlogService
    {
        public BlogService(HttpClient httpClient, IJSRuntime js, AppState appState)
        {
            HttpClient = httpClient;
            JS = js;
            AppState = appState;
        }

        public HttpClient HttpClient { get; }
        public IJSRuntime JS { get; }
        public AppState AppState { get; }

        public async Task GetBlogs(int page)
        {
            AppState.SetUserInfo(await JS.GetUserInfoAsync());
            var token = AppState.UserInfo.AccessToken;
            HttpClient.DefaultRequestHeaders.Remove("Authorization");
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await HttpClient.GetProtobufAsync<PaginatedBlogDto>($"api/blogs?page={page}");
            AppState.SetBlogs(response.Items);
        }
    }
}
