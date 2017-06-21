using System.Collections.Generic;
using System.Linq;
using BlogCore.Api.Blogs.Shared;
using BlogCore.Core;
using BlogCore.Core.Blogs.ListOutBlogs;

namespace BlogCore.Api.Blogs.ListOutBlogs
{
    public class ListOfBlogPresenter : IEnumerableOutputBoundary<
        IEnumerable<ListOfBlogResponseMsg>,
        IEnumerable<BlogItemViewModel>>
    {
        public IEnumerable<BlogItemViewModel> Transform(IEnumerable<ListOfBlogResponseMsg> input)
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