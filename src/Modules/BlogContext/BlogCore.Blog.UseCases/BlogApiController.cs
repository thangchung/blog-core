using BlogCore.Blog.UseCases.CreateBlog;
using BlogCore.Blog.UseCases.ListOutBlogByOwner;
using BlogCore.Blog.UseCases.UpdateBlogSetting;
using BlogCore.Core;
using BlogCore.Infrastructure.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [Authorize("User")]
        [HttpPost]
        public async Task<CreateBlogResponse> Post([FromBody] CreateBlogRequestMsg blogRequest)
        {
            return await _eventAggregator.Send(blogRequest);
        }

        [HttpPut("user/blog/{blogId:guid}/setting")]
        public async Task UpdateSetting(Guid blogId, [FromBody] UpdateBlogSettingRequest inputModel)
        {
            inputModel.BlogId = blogId;
            await _eventAggregator.Send(inputModel);
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
}