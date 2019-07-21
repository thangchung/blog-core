using BlogCore.Shared;
using BlogCore.Shared.v1;
using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Common;
using System;
using System.Threading.Tasks;

namespace BlogCore.Hosts.Web.Client.Services.Impl
{
    public class BlogService : ServiceBase
    {
        public BlogService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ProtoResultModel<PaginatedItemResponse>> GetBlogs(int page)
        {
            var httpClient = await SecureHttpClientAsync();
            return await httpClient.GetProtoAsync<PaginatedItemResponse>($"api/@blogs?page={page}");
        }

        public async Task<ProtoResultModel<RetrieveBlogResponse>> GetBlogById(Guid blogId)
        {
            var httpClient = await SecureHttpClientAsync();
            return await httpClient.GetProtoAsync<RetrieveBlogResponse>($"api/@blogs/{blogId}");
        }

        public async Task<ProtoResultModel<CreateBlogResponse>> CreateBlog(CreateBlogRequest model)
        {
            var httpClient = await SecureHttpClientAsync();
            return await httpClient.PostProtoAsync<CreateBlogResponse>($"api/@blogs", model);
        }
    }
}
