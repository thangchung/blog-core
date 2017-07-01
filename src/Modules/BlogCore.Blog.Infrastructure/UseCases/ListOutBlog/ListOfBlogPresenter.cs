using System.Collections.Generic;
using System.Linq;
using BlogCore.Blog.Infrastructure.UseCases.Shared;
using BlogCore.Core;

namespace BlogCore.Blog.Infrastructure.UseCases.ListOutBlog
{
    public class ListOfBlogPresenter : IEnumerableOutputBoundary<
        IEnumerable<ListOfBlogResponse>,
        IEnumerable<BlogItemViewModel>>
    {
        public IEnumerable<BlogItemViewModel> Transform(IEnumerable<ListOfBlogResponse> input)
        {
            return input.Select(x => new BlogItemViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Image = x.Image,
                Theme = x.Theme
            });
        }
    }
}