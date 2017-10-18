using BlogCore.Core;
using BlogCore.Core.Helpers;
using BlogCore.PostContext.Core.Domain;
using BlogCore.PostContext.UseCases.ListOutPostByBlog;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogCore.PostContext
{
    [Produces("application/json")]
    [Route("public/api/blogs")]
    public class PostApiPublicController : Controller
    {
        private readonly IMediator _eventAggregator;
        private readonly ListOutPostByBlogInteractor _listOutPostByBlogInteractor;
        private readonly ListOutPostByBlogPresenter _listOutPostByBlogPresenter;

        public PostApiPublicController(
            IMediator eventAggregator,
            ListOutPostByBlogInteractor listOutPostByBlogInteractor,
            ListOutPostByBlogPresenter listOutPostByBlogPresenter)
        {
            _eventAggregator = eventAggregator;
            _listOutPostByBlogInteractor = listOutPostByBlogInteractor;
            _listOutPostByBlogPresenter = listOutPostByBlogPresenter;
        }

        [HttpGet("{blogId:guid}/posts")]
        public async Task<PaginatedItem<ListOutPostByBlogResponse>> GetForBlog(Guid blogId, [FromQuery] int page)
        {
            var result = _listOutPostByBlogInteractor.Process(new ListOutPostByBlogRequest(blogId, page <= 0 ? 1 : page));
            return await _listOutPostByBlogPresenter.Transform(result);
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
