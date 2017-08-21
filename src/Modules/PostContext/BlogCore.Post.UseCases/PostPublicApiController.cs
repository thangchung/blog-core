using BlogCore.Core;
using BlogCore.Core.Helpers;
using BlogCore.Post.Domain;
using BlogCore.Post.UseCases.ListOutPostByBlog;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogCore.Post.UseCases
{
    [Route("api/public/blog")]
    public class PostPublicApiController : Controller
    {
        private readonly IMediator _eventAggregator;

        public PostPublicApiController(IMediator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        [HttpGet("{blogId:guid}/posts")]
        public async Task<PaginatedItem<ListOutPostByBlogResponse>> GetForBlog(Guid blogId, [FromQuery] int page)
        {
            if (page <= 0) page = 1;
            return await _eventAggregator.Send(new ListOutPostByBlogRequest(blogId, page));
        }

        [HttpGet("{blogId:guid}/posts/{postId:guid}")]
        public Domain.Post Get(Guid blogId, Guid postId)
        {
            return Domain.Post.CreateInstance(
                new BlogId(IdHelper.GenerateId("34C96712-2CDF-4E79-9E2F-768CB68DD552")),
                "sample title",
                "sample excerpt",
                "sample body",
                new AuthorId(IdHelper.GenerateId("4B5F26CE-DF97-494C-B747-121D215847D8")));
        }
    }
}
