using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Blog.Presenters.Shared;
using BlogCore.Blog.UseCases.ListOutBlog;
using BlogCore.Core;

namespace BlogCore.Blog.Presenters.ListOutBlog
{
    public class ListOfBlogPresenter : IEnumerableOutputBoundary<
        IEnumerable<ListOutBlogResponse>,
        IEnumerable<BlogItemViewModel>>
    {
        public Task<IEnumerable<BlogItemViewModel>> TransformAsync(IEnumerable<ListOutBlogResponse> responses)
        {
            var result = responses.Select(x => new BlogItemViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Image = x.Image,
                Theme = x.Theme
            });
            return Task.FromResult(result);
        }
    }
}