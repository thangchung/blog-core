using BlogCore.Shared;
using BlogCore.Shared.v1;
using BlogCore.Shared.v1.Blog;
using System;
using System.Threading.Tasks;

namespace BlogCore.Hosts.Web.Client.Services.Impl
{
    public class BlogService : ServiceBase
    {
        public BlogService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ProtoResultModel<GetBlogInfoResponse>> GetBlogInfo(Guid blogId)
        {
            return await HttpClient.GetProtoAsync<GetBlogInfoResponse>($"api/blogs/{blogId}/info");
        }

        public async Task<ProtoResultModel<RetrieveBlogsResponse>> GetBlogs(int page)
        {
            var httpClient = await SecureHttpClientAsync();
            return await httpClient.GetProtoAsync<RetrieveBlogsResponse>($"api/@blogs?page={page}");
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
