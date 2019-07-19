using BlogCore.Shared;
using BlogCore.Shared.v1;
using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Common;
using Microsoft.JSInterop;
using System;
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

        public async Task<ProtoResultModel<PaginatedItemResponse>> GetBlogs(int page)
        {
            await SetHeader();
            return await HttpClient.GetProtobufAsync<ProtoResultModel<PaginatedItemResponse>, PaginatedItemResponse>($"api/blogs?page={page}");
        }

        public async Task<ProtoResultModel<RetrieveBlogResponse>> GetBlogById(Guid blogId)
        {
            await SetHeader();
            return await HttpClient.GetProtobufAsync<ProtoResultModel<RetrieveBlogResponse>, RetrieveBlogResponse>($"api/blogs/{blogId}");
        }

        public async Task<ProtoResultModel<CreateBlogResponse>> CreateBlog(CreateBlogRequest model)
        {
            await SetHeader();
            return await HttpClient.PostProtobufAsync<ProtoResultModel<CreateBlogResponse>, CreateBlogResponse>($"api/blogs", model);
        }
    }
}
