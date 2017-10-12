using BlogCore.Api.Features.Posts.ListOutPostByBlog;
using BlogCore.Core;
using BlogCore.Core.Helpers;
using BlogCore.PostContext.Domain;
using BlogCore.PostContext.UseCases.ListOutPostByBlog;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogCore.Api.Features.Posts
{
    [Route("public/api/blogs")]
    public class PublicPostApiController : Controller
    {
        private readonly IMediator _eventAggregator;
        private readonly ListOutPostByBlogPresenter _listOutPostByBlogPresenter;

        public PublicPostApiController(IMediator eventAggregator, ListOutPostByBlogPresenter listOutPostByBlogPresenter)
        {
            _eventAggregator = eventAggregator;
            _listOutPostByBlogPresenter = listOutPostByBlogPresenter;
        }

        [HttpGet("{blogId:guid}/posts")]
        public async Task<PaginatedItem<ListOutPostByBlogResponse>> GetForBlog(Guid blogId, [FromQuery] int page)
        {
            var postResult = await _eventAggregator.Send(new ListOutPostByBlogRequest(blogId, page <= 0 ? 1 : page));
            return _listOutPostByBlogPresenter.Transform(postResult);
        }

        [HttpGet("{blogId:guid}/posts/{postId:guid}")]
        public Post Get(Guid blogId, Guid postId)
        {
            return Post.CreateInstance(
                new BlogId(IdHelper.GenerateId("34C96712-2CDF-4E79-9E2F-768CB68DD552")),
                "sample title",
                "sample excerpt",
                "sample body",
                new AuthorId(IdHelper.GenerateId("4B5F26CE-DF97-494C-B747-121D215847D8")));
        }
    }
}
