using BlogCore.Infrastructure.UseCase;
using FluentValidation;
using System;

namespace BlogCore.BlogContext.UseCases.BasicCrud
{
    public class UpdateBlogRequest : IRequest<UpdateBlogResponse>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Theme { get; set; } = 1; //default
    }

    public class UpdateBlogResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Theme { get; set; } = 1; //default
    }

    public class UpdateBlogRequestValidator : AbstractValidator<UpdateBlogRequest>
    {
        public UpdateBlogRequestValidator()
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
                .Must(x => x == 1)
                .WithMessage("Theme should be 1.");
        }
    }
}
