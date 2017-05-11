using BlogCore.Core;
using BlogCore.Core.Blogs.GetBlog;

namespace BlogCore.Web.Blogs
{
    public class GetBlogPresenter : IObjectOutputBoundary<GetBlogResponseMsg, BlogItemViewModel>
    {
        public BlogItemViewModel Transform(GetBlogResponseMsg input)
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