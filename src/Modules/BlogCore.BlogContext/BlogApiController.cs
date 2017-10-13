using BlogCore.BlogContext.UseCases.Crud;
using BlogCore.BlogContext.UseCases.ListOutBlogByOwner;
using BlogCore.BlogContext.UseCases.UpdateBlogSetting;
using BlogCore.Core;
using BlogCore.Infrastructure.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogCore.BlogContext
{
    [Route("api/blogs")]
    public class BlogApiController : AuthorizedController
    {
        private readonly IMediator _eventAggregator;

        public BlogApiController(IMediator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("owner")]
        public async Task<PaginatedItem<ListOutBlogByOwnerResponse>> GetOwner()
        {
            return await _eventAggregator.Send(new ListOutBlogByOwnerRequest());
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<CreateBlogResponse> Post([FromBody] CreateBlogRequest blogRequest)
        {
            return await _eventAggregator.Send(blogRequest);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("user/blog/{blogId:guid}/setting")]
        public async Task UpdateSetting(Guid blogId, [FromBody] UpdateBlogSettingRequest inputModel)
        {
            inputModel.BlogId = blogId;
            await _eventAggregator.Send(inputModel);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}