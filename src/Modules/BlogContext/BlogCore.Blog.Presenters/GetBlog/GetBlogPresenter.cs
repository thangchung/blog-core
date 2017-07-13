using System.Threading.Tasks;
using BlogCore.Blog.Presenters.Shared;
using BlogCore.Blog.UseCases.GetBlog;
using BlogCore.Core;

namespace BlogCore.Blog.Presenters.GetBlog
{
    public class GetBlogPresenter : IObjectOutputBoundary<GetBlogResponse, BlogItemViewModel>
    {
        public Task<BlogItemViewModel> TransformAsync(GetBlogResponse input)
        {
            return Task.FromResult(new BlogItemViewModel
            {
                Id = input.Id,
                Title = input.Title,
                Description = input.Description,
                Image = input.Image,
                Theme = input.Theme
            });
        }
    }
}