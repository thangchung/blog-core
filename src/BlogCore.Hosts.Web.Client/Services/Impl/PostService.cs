using BlogCore.Shared;
using BlogCore.Shared.v1;
using BlogCore.Shared.v1.Post;
using System;
using System.Threading.Tasks;

namespace BlogCore.Hosts.Web.Client.Services.Impl
{
    public class PostService : ServiceBase
    {
        public PostService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ProtoResultModel<GetPostsByBlogResponse>> GetPostsByBlog(Guid blogId, int page)
        {
            return await HttpClient.GetProtoAsync<GetPostsByBlogResponse>($"api/posts/{blogId}/posts?page={page}");
        }
    }
}
