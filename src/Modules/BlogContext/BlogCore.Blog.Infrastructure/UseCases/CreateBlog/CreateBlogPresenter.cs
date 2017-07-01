using BlogCore.Core;

namespace BlogCore.Blog.Infrastructure.UseCases.CreateBlog
{
    public class CreateBlogPresenter : 
        IObjectOutputBoundary<CreateBlogResponse, CategoryCreatedViewModel>
    {
        public CategoryCreatedViewModel Transform(CreateBlogResponse input)
        {
            return new CategoryCreatedViewModel
            {
                BlogId = input.BlogId
            };
        }
    }
}