using System.Threading.Tasks;
using BlogCore.Blog.UseCases.CreateBlog;
using BlogCore.Core;

namespace BlogCore.Blog.Presenters.CreateBlog
{
    public class CreateBlogPresenter : 
        IObjectOutputBoundary<CreateBlogResponse, CategoryCreatedViewModel>
    {
        public Task<CategoryCreatedViewModel> TransformAsync(CreateBlogResponse input)
        {
            return Task.FromResult(new CategoryCreatedViewModel
            {
                BlogId = input.BlogId
            });
        }
    }
}