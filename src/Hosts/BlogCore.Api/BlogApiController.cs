using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogCore.Blog.UseCases.CreateBlog;
using BlogCore.Blog.UseCases.GetBlog;
using BlogCore.Blog.UseCases.ListOutBlog;
using BlogCore.Blog.UseCases.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Api
{
    [Route("api/blogs")]
    public class BlogApiController : Controller
    {
        private readonly ListOfBlogPresenter _listOfBlogPresenter;
        private readonly GetBlogPresenter _getBlogPresenter;
        private readonly CreateBlogPresenter _createBlogPresenter;

        private readonly IMediator _eventAggregator;

        public BlogApiController(
            IMediator eventAggregator, 
            ListOfBlogPresenter listOfBlogPresenter, 
            GetBlogPresenter getBlogPresenter,
            CreateBlogPresenter createBlogPresenter)
        {
            _eventAggregator = eventAggregator;
            _listOfBlogPresenter = listOfBlogPresenter;
            _getBlogPresenter = getBlogPresenter;
            _createBlogPresenter = createBlogPresenter;
        }

        [HttpGet]
        [Authorize("Admin")]
        public async Task<IEnumerable<BlogItemViewModel>> Get()
        {
            var blogResponses = await _eventAggregator.Send(new ListOfBlogRequest());
            return _listOfBlogPresenter.Transform(blogResponses);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<BlogItemViewModel> Get(Guid id)
        {
            var blogResponse = await _eventAggregator.Send(new GetBlogRequest(id));
            return _getBlogPresenter.Transform(blogResponse);
        }

        [HttpPost]
        [Authorize("User")]
        public async Task<CategoryCreatedViewModel> Post([FromBody] CreateBlogRequestMsg blogRequest)
        {
            var blogCreated = await _eventAggregator.Send(blogRequest);
            return _createBlogPresenter.Transform(blogCreated);
        }

        [HttpPut("{id}")]
        [Authorize("User")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        [Authorize("User")]
        public void Delete(int id)
        {
        }
    }
}