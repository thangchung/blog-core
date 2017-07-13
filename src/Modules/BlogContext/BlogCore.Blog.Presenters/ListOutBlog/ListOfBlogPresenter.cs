using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Blog.Presenters.Shared;
using BlogCore.Blog.UseCases.ListOutBlog;
using BlogCore.Core;

namespace BlogCore.Blog.Presenters.ListOutBlog
{
    public class ListOfBlogPresenter : IEnumerableOutputBoundary<
        IEnumerable<ListOfBlogResponse>,
        IEnumerable<BlogItemViewModel>>
    {
        public Task<IEnumerable<BlogItemViewModel>> TransformAsync(IEnumerable<ListOfBlogResponse> input)
        {
            var result= input.Select(x => new BlogItemViewModel
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