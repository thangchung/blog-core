using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogCore.Core.Blogs.CreateBlog;
using BlogCore.Core.Blogs.GetBlog;
using BlogCore.Core.Blogs.ListOutBlogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Api.Blogs
{
    [Route("api/blogs")]
    public class BlogApiController : Controller
    {
        private readonly BlogPresenterFactory _blogPresenterFactory;

        private readonly IMediator _eventAggregator;

        public BlogApiController(
            IMediator eventAggregator,
            BlogPresenterFactory blogPresenterFactory)
        {
            _eventAggregator = eventAggregator;
            _blogPresenterFactory = blogPresenterFactory;
        }

        [HttpGet]
        [Authorize("BlogsAdmin")]
        public async Task<IEnumerable<BlogItemViewModel>> Get()
        {
            var blogResponses = await _eventAggregator.Send(new ListOfBlogRequestMsg());
            return _blogPresenterFactory
                .ListOfBlogPresenterInstance()
                .Transform(blogResponses);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<BlogItemViewModel> Get(Guid id)
        {
            var blogResponse = await _eventAggregator.Send(new GetBlogRequestMsg {Id = id});
            return _blogPresenterFactory
                .GetBlogPresenterInstance()
                .Transform(blogResponse);
        }

        [HttpPost]
        [Authorize("BlogsUser")]
        public async Task<CategoryCreatedViewModel> Post([FromBody] CreateBlogRequestMsg blogRequest)
        {
            var blogCreated = await _eventAggregator.Send(blogRequest);
            return _blogPresenterFactory
                .CreateBlogPresenterInstance()
                .Transform(blogCreated);
        }

        [HttpPut("{id}")]
        [Authorize("BlogsUser")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        [Authorize("BlogsUser")]
        public void Delete(int id)
        {
        }
    }
}