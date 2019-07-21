using BlogCore.Shared.v1.Post;
using FluentValidation;

namespace BlogCore.Shared.v1.Validators.Posts
{
    public class GetPostsByBlogRequestValidator : AbstractValidator<GetPostsByBlogRequest>
    {
        public GetPostsByBlogRequestValidator()
        {

        }
    }
}
