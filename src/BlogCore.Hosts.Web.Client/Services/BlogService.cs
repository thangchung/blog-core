using BlogCore.Shared;
using BlogCore.Shared.v1.Blog;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogCore.Hosts.Web.Client.Services
{
    public class BlogService : BaseService
    {
        public BlogService(HttpClient httpClient, IJSRuntime js, AppState appState)
            : base(httpClient, js, appState)
        {
        }

        public async Task<PaginatedBlogDto> GetBlogs(int page)
        {
            await SetHeader();
            return await HttpClient.GetProtobufAsync<PaginatedBlogDto>($"api/blogs?page={page}");
        }
    }
}
