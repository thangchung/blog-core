using BlogCore.Core;
using BlogCore.Core.Blogs.CreateBlog;

namespace BlogCore.Api.Blogs
{
    public class CreateBlogPresenter : 
        IObjectOutputBoundary<CreateBlogResponseMsg, CategoryCreatedViewModel>
    {
        public CategoryCreatedViewModel Transform(CreateBlogResponseMsg input)
        {
            return new CategoryCreatedViewModel
            {
                BlogId = input.BlogId
            };
        }
    }
}