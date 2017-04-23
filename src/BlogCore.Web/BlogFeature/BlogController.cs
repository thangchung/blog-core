using System.Collections.Generic;
using System.Threading.Tasks;
using BlogCore.Core.BlogFeature;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Web.BlogFeature
{
    [Route("api/blogs")]
    public class BlogController : Controller
    {
        private readonly BlogInteractor _blogUseCase;
        private readonly BlogPresenter _blogPresenter;

        public BlogController(BlogInteractor blogUseCase, BlogPresenter blogPresenter)
        {
            _blogUseCase = blogUseCase;
            _blogPresenter = blogPresenter;
        }

        [HttpGet]
        public async Task<IEnumerable<ListOfBlogViewModel>> Get()
        {
            var blogResponses = await _blogUseCase.Handle(new ListOfBlogRequestMsg());
            var viewModel = _blogPresenter.Handle(blogResponses);
            return viewModel;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "blog";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
