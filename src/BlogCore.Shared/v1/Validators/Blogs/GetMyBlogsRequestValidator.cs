using BlogCore.Shared.v1.Blog;
using FluentValidation;

namespace BlogCore.Shared.v1.Blogs.Validators
{
    public class GetMyBlogsRequestValidator : AbstractValidator<GetMyBlogsRequest>
    {
        public GetMyBlogsRequestValidator()
        {
            RuleFor(x => x.Page)
                .Must(y => y > 0)
                .WithMessage("Page should be greater than zero.");

            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage("Username could not be null or empty.");
        }
    }
}
