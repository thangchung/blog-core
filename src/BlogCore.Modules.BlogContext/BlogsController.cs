using BlogCore.Shared.v1.Blog;
using Microsoft.AspNetCore.Mvc;
using NetCoreKit.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        [HttpGet("owner")]
        public async Task<ActionResult<PaginatedItem<GetBlogByUserResponse>>> GetOwner()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedItem<RetrieveBlogsResponse>>> GetByPage([FromQuery] int page = 1)
        {
            var blogs = new List<RetrieveBlogsResponse>();
            var blog = new RetrieveBlogsResponse();
            blog.Blogs.Add(new BlogDto
            {
                Id = Guid.NewGuid().ToString(),
                Title = "My blog",
                Description = "This is my blog",
                Image = "/images/my-blog.png",
                Theme = 1
            });
            blogs.Add(blog);
            var pager = new PaginatedItem<RetrieveBlogsResponse>(1, 1, blogs);
            return Ok(await Task.FromResult(pager));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RetrieveBlogResponse>> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{username:required}")]
        public async Task<ActionResult<PaginatedItem<GetMyBlogsResponse>>> GetBlogByUserName(string username, [FromQuery]int page = 1)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<CreateBlogResponse>> CreateBlog([FromBody] CreateBlogRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{blogId:guid}/users/{userId:guid}/setting")]
        public async Task<ActionResult<UpdateBlogSettingResponse>> UpdateBlogSetting(Guid blogId, Guid userId, [FromBody] UpdateBlogSettingRequest inputModel)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UpdateBlogResponse>> Put(Guid id, [FromBody] UpdateBlogRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<DeleteBlogResponse>> Delete(Guid id, [FromBody] DeleteBlogRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
