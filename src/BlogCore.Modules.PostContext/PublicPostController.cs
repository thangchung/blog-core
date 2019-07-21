using BlogCore.Shared.v1;
using BlogCore.Shared.v1.Guard;
using BlogCore.Shared.v1.Post;
using BlogCore.Shared.v1.Usecase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext
{
    [Route("api/posts")]
    [ApiController]
    public class PublicPostController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public PublicPostController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet("{blogId:guid}/posts")]
        public async Task<ActionResult<ProtoResultModel<GetPostsByBlogResponse>>> GetPosts(Guid blogId, [FromQuery] int page)
        {
            var useCase = _serviceProvider.GetService<IUseCase<GetPostsByBlogRequest, GetPostsByBlogResponse>>().NotNull();
            var response = await useCase.ExecuteAsync(new GetPostsByBlogRequest
            {
                BlogId = blogId.ToString(),
                Page = page
            });
            return new OkObjectResult(new ProtoResultModel<GetPostsByBlogResponse>(response));
        }

        [HttpGet("{blogId:guid}/posts/{postId:guid}")]
        public async Task<ActionResult<ProtoResultModel<PostDto>>> GetPost(Guid blogId, Guid postId)
        {
            throw new NotImplementedException();
        }
    }
}
