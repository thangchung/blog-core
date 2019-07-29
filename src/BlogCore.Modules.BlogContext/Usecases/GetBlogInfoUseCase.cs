using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Usecase;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext.Usecases
{
    public class GetBlogInfoUseCase : IUseCase<GetBlogInfoRequest, GetBlogInfoResponse>
    {
        public async Task<GetBlogInfoResponse> ExecuteAsync(GetBlogInfoRequest request)
        {
            var blog = new BlogDto { Id = request.BlogId, Title = $"My Blog {request.BlogId}", Description = $"This is description for {request.BlogId}" };
            return await Task.FromResult(new GetBlogInfoResponse { Blog = blog });
        }
    }
}
