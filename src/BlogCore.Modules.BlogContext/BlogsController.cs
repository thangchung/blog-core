using BlogCore.Shared;
using BlogCore.Shared.v1.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreKit.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext
{
    [Authorize]
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
        public async Task<ActionResult<PaginatedItem<BlogDto>>> GetByPage([FromQuery] int page = 1)
        {
            var blogs = new List<BlogDto>();
            blogs.Add(new BlogDto
            {
                Id = Guid.NewGuid().ToString(),
                Title = "My blog 1",
                Description = "This is my blog 1",
                Image = "/images/my-blog-1.png",
                Theme = 1
            });

            blogs.Add(new BlogDto
            {
                Id = Guid.NewGuid().ToString(),
                Title = "My blog 2",
                Description = "This is my blog 2",
                Image = "/images/my-blog-2.png",
                Theme = 2
            });

            var pager = new PaginatedBlogDto();
            pager.Items.AddRange(blogs);
            pager.TotalItems = 1;
            pager.TotalPages = 1;

            return Ok(await Task.FromResult(pager.SerializeProtobufToJson()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RetrieveBlogResponse>> Get(Guid id)
        {
            var response = new RetrieveBlogResponse();
            response.Blog = new BlogDto
            {
                Id = id.ToString(),
                Title = "My blog",
                Description = "This is my blog",
                Image = "/images/my-blog.png",
                Theme = 1
            };

            return Ok(await Task.FromResult(response.SerializeProtobufToJson()));
        }

        [HttpGet("username/{username:required}")]
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
