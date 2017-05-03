using BlogCore.Core.ManageBlog;
using FluentValidation;

namespace BlogCore.Infrastructure.ManageBlog
{
    public class CreateBlogRequestMsgValidator : AbstractValidator<CreateBlogRequestMsg>
    {
        public CreateBlogRequestMsgValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Title could not be null or empty.")
                .Must(x => !string.IsNullOrEmpty(x) && x.Length <= 20)
                .WithMessage("Title should be between 1 and 20 chars.");

            RuleFor(x => x.Description)
                .Must(x => !string.IsNullOrEmpty(x) && x.Length <= 50)
                .WithMessage("Description should be between 1 and 50 chars.");

            RuleFor(x => x.Theme)
                .Must(x => !string.IsNullOrEmpty(x) && x.Length <= 10)
                .WithMessage("Theme should be between 1 and 10.");

            RuleFor(x => x.PostsPerPage)
                .Must(x => x > 0 && x < int.MaxValue)
                .WithMessage($"PostsPerPage should be between 1 and {int.MaxValue}.");

            RuleFor(x => x.DaysToComment)
                .Must(x => x > 0 && x < 365)
                .WithMessage("PostsPerPage should be between 1 and 365.");
        }  
    }
}