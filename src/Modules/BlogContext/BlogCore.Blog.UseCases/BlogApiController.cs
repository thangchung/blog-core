using System;
using System.Threading.Tasks;
using BlogCore.Blog.UseCases.CreateBlog;
using BlogCore.Blog.UseCases.GetBlog;
using BlogCore.Blog.UseCases.ListOutBlog;
using BlogCore.Blog.UseCases.ListOutBlogByOwner;
using BlogCore.Blog.UseCases.UpdateBlogSetting;
using BlogCore.Core;
using BlogCore.Infrastructure.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Blog.UseCases
{
    [Route("api/blogs")]
    public class BlogApiController : AuthorizedController
    {
        private readonly IMediator _eventAggregator;

        public BlogApiController(IMediator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        [Authorize("Admin")]
        [HttpGet("owner")]
        public async Task<PaginatedItem<ListOutBlogByOwnerResponse>> GetOwner()
        {
            return await _eventAggregator.Send(new ListOutBlogByOwnerRequest());
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<PaginatedItem<ListOutBlogResponse>> GetByPage([FromQuery] int page)
        {
            return await _eventAggregator.Send(new ListOutBlogRequest(page));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<GetBlogResponse> Get(Guid id)
        {
            return await _eventAggregator.Send(new GetBlogRequest(id));
        }

        [Authorize("User")]
        [HttpPost]
        public async Task<CreateBlogResponse> Post([FromBody] CreateBlogRequestMsg blogRequest)
        {
            return await _eventAggregator.Send(blogRequest);
        }

        [AllowAnonymous]
        [HttpPut("user/{userId}/setting")]
        public async Task UpdateSetting(Guid userId, [FromBody] BlogSettingInputModel inputModel)
        {
            await _eventAggregator.Send(new UpdateBlogSettingRequest
            {
                BlogId = inputModel.BlogId,
                DaysToComment = inputModel.DaysToComment,
                ModerateComments = inputModel.ModerateComments,
                PostsPerPage = inputModel.PostsPerPage
            });
        }

        [Authorize("User")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [Authorize("User")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class BlogSettingInputModel
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Bio { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public Guid BlogId { get; set; }
        public int PostsPerPage { get; set; }
        public int DaysToComment { get; set; }
        public bool ModerateComments { get; set; }
    }
}