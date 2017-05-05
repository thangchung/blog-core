using System.Collections.Generic;
using System.Threading.Tasks;
using BlogCore.Core.Blogs.CreateBlog;
using BlogCore.Core.Blogs.ListOfBlog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Web.Blogs
{
    [Route("api/blogs")]
    public class BlogController : Controller
    {
        private readonly BlogPresenter _blogPresenter;
        private readonly IMediator _mediator;

        public BlogController(BlogPresenter blogPresenter, IMediator mediator)
        {
            _blogPresenter = blogPresenter;
            _mediator = mediator;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IEnumerable<ListOfBlogViewModel>> Get()
        {
            var blogResponses = await _mediator.Send(new ListOfBlogRequestMsg());
            var viewModel = _blogPresenter.Handle(blogResponses);
            return viewModel;
        }

        [HttpGet("{id}"), AllowAnonymous]
        public string Get(int id)
        {
            return "blog";
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<CategoryCreatedViewModel> Post([FromBody] CreateBlogRequestMsg blogRequest)
        {
            var blogCreated = await _mediator.Send(blogRequest);
            var viewModel = _blogPresenter.Handle(blogCreated);
            return viewModel;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public void Delete(int id)
        {
        }
    }
}