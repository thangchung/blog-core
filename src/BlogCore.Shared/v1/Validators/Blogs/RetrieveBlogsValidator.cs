using BlogCore.Shared.v1.Blog;
using FluentValidation;

namespace BlogCore.Shared.v1.Blogs.Validators
{
    public class RetrieveBlogsValidator : AbstractValidator<RetrieveBlogsRequest>
    {
        public RetrieveBlogsValidator()
        {
            RuleFor(x => x.CurrentPage)
                .Must(y => y > 0)
                .WithMessage("CurrentPage should be greater than zero.");
        }
    }
}
