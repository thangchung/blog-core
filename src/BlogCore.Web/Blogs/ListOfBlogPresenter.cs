using System.Collections.Generic;
using System.Linq;
using BlogCore.Core;
using BlogCore.Core.Blogs.ListOfBlog;

namespace BlogCore.Web.Blogs
{
    public class ListOfBlogPresenter : IEnumerableOutputBoundary<IEnumerable<ListOfBlogResponseMsg>, IEnumerable<BlogItemViewModel>>
    {
        public IEnumerable<BlogItemViewModel> Transform(IEnumerable<ListOfBlogResponseMsg> input)
        {
            return input.Select(x => new BlogItemViewModel
            {
                Title = x.Title,
                Description = x.Description,
                Image = x.Image
            });
        }
    }
}