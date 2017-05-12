using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogCore.Core;
using BlogCore.Core.Blogs.CreateBlog;
using BlogCore.Core.Blogs.GetBlog;
using BlogCore.Core.Blogs.ListOfBlog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Web.Blogs
{
    [Route("api/blogs")]
    public class BlogApiController : Controller
    {
        private readonly IObjectOutputBoundary<
            CreateBlogResponseMsg,
            CategoryCreatedViewModel> _blogCreatedPresenter;

        private readonly IObjectOutputBoundary<
            GetBlogResponseMsg,
            BlogItemViewModel> _getBlogPresenter;

        private readonly IEnumerableOutputBoundary<
            IEnumerable<ListOfBlogResponseMsg>,
            IEnumerable<BlogItemViewModel>> _listOfBlogPresenter;

        private readonly IMediator _useCases;

        public BlogApiController(
            IMediator useCases,
            IObjectOutputBoundary<CreateBlogResponseMsg, CategoryCreatedViewModel> blogCreatedPresenter,
            IEnumerableOutputBoundary<IEnumerable<ListOfBlogResponseMsg>, IEnumerable<BlogItemViewModel>> listOfBlogPresenter,
            IObjectOutputBoundary<GetBlogResponseMsg, BlogItemViewModel> getBlogPresenter)
        {
            _useCases = useCases;
            _blogCreatedPresenter = blogCreatedPresenter;
            _listOfBlogPresenter = listOfBlogPresenter;
            _getBlogPresenter = getBlogPresenter;
        }

        [HttpGet]
        [Authorize("BlogsAdmin")]
        public async Task<IEnumerable<BlogItemViewModel>> Get()
        {
            var blogResponses = await _useCases.Send(new ListOfBlogRequestMsg());
            return _listOfBlogPresenter.Transform(blogResponses);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<BlogItemViewModel> Get(Guid id)
        {
            var blogResponse = await _useCases.Send(new GetBlogRequestMsg {Id = id});
            return _getBlogPresenter.Transform(blogResponse);
        }

        [HttpPost]
        [Authorize("BlogsUser")]
        public async Task<CategoryCreatedViewModel> Post([FromBody] CreateBlogRequestMsg blogRequest)
        {
            var blogCreated = await _useCases.Send(blogRequest);
            return _blogCreatedPresenter.Transform(blogCreated);
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