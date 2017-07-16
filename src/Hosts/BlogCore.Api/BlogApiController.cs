using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogCore.Blog.Presenters.CreateBlog;
using BlogCore.Blog.Presenters.GetBlog;
using BlogCore.Blog.Presenters.ListOutBlog;
using BlogCore.Blog.Presenters.ListOutBlogByOwner;
using BlogCore.Blog.Presenters.Shared;
using BlogCore.Blog.UseCases.CreateBlog;
using BlogCore.Blog.UseCases.GetBlog;
using BlogCore.Blog.UseCases.ListOutBlog;
using BlogCore.Blog.UseCases.ListOutBlogByOwner;
using BlogCore.Infrastructure.AspNetCore;
using BlogCore.Post.Presenters.ListOutPostByBlog;
using BlogCore.Post.UseCases.ListOutPostByBlog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Api
{
    [Route("api/blogs")]
    public class BlogApiController : AuthorizedController
    {
        private readonly ListOfBlogByOwnerPresenter _listOfBlogByOwnerPresenter;
        private readonly ListOfBlogPresenter _listOfBlogPresenter;
        private readonly GetBlogPresenter _getBlogPresenter;
        private readonly CreateBlogPresenter _createBlogPresenter;
        private readonly ListOutPostByBlogPresenter _listOutPostByBlogPresenter;

        private readonly IMediator _eventAggregator;

        public BlogApiController(
            IMediator eventAggregator,
            ListOfBlogByOwnerPresenter listOfBlogByOwnerPresenter,
            ListOfBlogPresenter listOfBlogPresenter,
            GetBlogPresenter getBlogPresenter,
            CreateBlogPresenter createBlogPresenter, 
            ListOutPostByBlogPresenter listOutPostByBlogPresenter)
        {
            _eventAggregator = eventAggregator;
            _listOfBlogByOwnerPresenter = listOfBlogByOwnerPresenter;
            _listOfBlogPresenter = listOfBlogPresenter;
            _getBlogPresenter = getBlogPresenter;
            _createBlogPresenter = createBlogPresenter;
            _listOutPostByBlogPresenter = listOutPostByBlogPresenter;
        }

        [Authorize("Admin")]
        [HttpGet("owner")]
        public async Task<IEnumerable<BlogItemViewModel>> GetOwner()
        {
            var blogResponses = await _eventAggregator.Send(new ListOutBlogByOwnerRequest());
            return await _listOfBlogByOwnerPresenter.TransformAsync(blogResponses);
        }

        [AllowAnonymous]
        [HttpGet("paged/{page}")]
        public async Task<IEnumerable<BlogItemViewModel>> GetByPage(int page)
        {
            var blogResponses = await _eventAggregator.Send(new ListOutBlogRequest(page));
            return await _listOfBlogPresenter.TransformAsync(blogResponses);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<BlogItemViewModel> Get(Guid id)
        {
            var blogResponse = await _eventAggregator.Send(new GetBlogRequest(id));
            return await _getBlogPresenter.TransformAsync(blogResponse);
        }

        [AllowAnonymous]
        [HttpGet("{blogId}/posts")]
        public async Task<ListOfPostByBlogViewModel> GetForBlog(Guid blogId, [FromQuery] int page)
        {
            var responses = await _eventAggregator.Send(new ListOutPostByBlogRequest(blogId, page));
            return await _listOutPostByBlogPresenter.TransformAsync(responses);
        }

        [Authorize("User")]
        [HttpPost]
        public async Task<CategoryCreatedViewModel> Post([FromBody] CreateBlogRequestMsg blogRequest)
        {
            var blogCreated = await _eventAggregator.Send(blogRequest);
            return await _createBlogPresenter.TransformAsync(blogCreated);
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