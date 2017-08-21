using BlogCore.Blog.UseCases.GetBlog;
using BlogCore.Blog.UseCases.ListOutBlog;
using BlogCore.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogCore.Blog.UseCases
{
    [Route("api/public/blogs")]
    public class BlogPublicApiController : Controller
    {
        private readonly IMediator _eventAggregator;

        public BlogPublicApiController(IMediator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        [HttpGet("")]
        public async Task<PaginatedItem<ListOutBlogResponse>> GetByPage([FromQuery] int page)
        {
            return await _eventAggregator.Send(new ListOutBlogRequest(page));
        }

        [HttpGet("{id:guid}")]
        public async Task<GetBlogResponse> Get(Guid id)
        {
            return await _eventAggregator.Send(new GetBlogRequest(id));
        }
    }
}
