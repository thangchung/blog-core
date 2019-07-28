using BlogCore.Shared.v1.Blog;
using FluentValidation;

namespace BlogCore.Shared.v1.Blogs.Validators
{
    public class CreateBlogRequestValidator : AbstractValidator<CreateBlogRequest>
    {
        public CreateBlogRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("The title could not be null or empty.");

            RuleFor(x => x.OwnerEmail)
                .NotNull()
                .NotEmpty()
                .WithMessage("The email of owner could not be null or empty.");

            RuleFor(x => x.BlogSetting)
                .NotNull()
                .NotEmpty()
                .WithMessage("The blog setting could not be null or empty.");

            RuleFor(x => x.BlogSetting.PostsPerPage)
                .GreaterThan(0)
                .WithMessage("The PostsPerPage in BlogSetting could not be less than zero.");

            RuleFor(x => x.BlogSetting.PostsPerPage)
                .LessThanOrEqualTo(20)
                .WithMessage("PostsPerPage in BlogSetting could not be greater than 20 posts.");

            RuleFor(x => x.BlogSetting.DaysToComment)
                .GreaterThan(0)
                .WithMessage("The DaysToComment in BlogSetting could not be less than zero.");

            RuleFor(x => x.BlogSetting.DaysToComment)
                .LessThanOrEqualTo(10)
                .WithMessage("DaysToComment in BlogSetting could not be greater than 10 days.");
        }
    }
}
