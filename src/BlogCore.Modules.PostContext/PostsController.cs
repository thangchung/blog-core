using BlogCore.Shared.v1.Post;
using Microsoft.AspNetCore.Mvc;
using NetCoreKit.Domain;
using System;
using System.Threading.Tasks;

namespace BlogCore.Modules.PostContext
{
    [Route("api/blogs")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        [HttpGet("{blogId:guid}/posts")]
        public async Task<ActionResult<PaginatedItem<GetPostsByBlogResponse>>> GetPostForBlog(Guid blogId, [FromQuery] int page)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{blogId:guid}/posts/{postId:guid}")]
        public async Task<ActionResult<GetSpecificPostForBlogResponse>> GetSpecificPostForBlog(Guid blogId, Guid postId)
        {
            throw new NotImplementedException();
        }
    }
}
