using BlogCore.Shared.v1.Blog;
using FluentValidation;

namespace BlogCore.Shared.v1.Validators
{
    public class CreateBlogRequestValidator : AbstractValidator<CreateBlogRequest>
    {
        public CreateBlogRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Title could not be null or empty.");
        }
    }
}
