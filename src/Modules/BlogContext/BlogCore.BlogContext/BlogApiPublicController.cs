using BlogCore.BlogContext.UseCases.Crud;
using BlogCore.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogCore.BlogContext
{
    [Route("public/api/blogs")]
    public class BlogApiPublicController : Controller
    {
        private readonly IMediator _eventAggregator;

        public BlogApiPublicController(IMediator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        [HttpGet("")]
        public async Task<PaginatedItem<RetrieveBlogsResponse>> GetByPage([FromQuery] int page)
        {
            return await _eventAggregator.Send(new RetrieveBlogsRequest(page));
        }

        [HttpGet("{id:guid}")]
        public async Task<RetrieveBlogResponse> Get(Guid id)
        {
            return await _eventAggregator.Send(new RetrieveBlogRequest(id));
        }
    }
}
