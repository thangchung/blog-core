using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Blog.Presenters.Shared;
using BlogCore.Blog.UseCases.ListOutBlogByOwner;
using BlogCore.Core;

namespace BlogCore.Blog.Presenters.ListOutBlogByOwner
{
    public class ListOfBlogByOwnerPresenter : IEnumerableOutputBoundary<
        IEnumerable<ListOutBlogByOwnerResponse>,
        IEnumerable<BlogItemViewModel>>
    {
        public Task<IEnumerable<BlogItemViewModel>> TransformAsync(IEnumerable<ListOutBlogByOwnerResponse> responses)
        {
            var result= responses.Select(x => new BlogItemViewModel
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