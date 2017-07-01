using BlogCore.Blog.Infrastructure.UseCases.Shared;
using BlogCore.Core;

namespace BlogCore.Blog.Infrastructure.UseCases.GetBlog
{
    public class GetBlogPresenter : IObjectOutputBoundary<GetBlogResponse, BlogItemViewModel>
    {
        public BlogItemViewModel Transform(GetBlogResponse input)
        {
            return new BlogItemViewModel
            {
                Id = input.Id,
                Title = input.Title,
                Description = input.Description,
                Image = input.Image,
                Theme = input.Theme
            };
        }
    }
}