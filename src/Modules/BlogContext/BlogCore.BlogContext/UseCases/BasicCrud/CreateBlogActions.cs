using BlogCore.Infrastructure.UseCase;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.BlogContext.UseCases.BasicCrud
{  
    public class CreateBlogRequest : IRequest<CreateBlogResponse>
    {
        public CreateBlogRequest(
            string title, 
            string description,
            int? theme, 
            int? postsPerPage, 
            int? daysToComment, 
            bool? moderateComments)
        {
            Title = title;
            Description = description;
            Theme = theme ?? 1;
            PostsPerPage = postsPerPage ?? 10;
            DaysToComment = daysToComment ?? 5;
            ModerateComments = moderateComments ?? true;
        }

        public string Title { get; }
        public string Description { get; }
        public int Theme { get; } 
        public int PostsPerPage { get; }
        public int DaysToComment { get; }
        public bool ModerateComments { get; }
    }

    public class CreateBlogResponse
    {
        public CreateBlogResponse(Guid blogId)
        {
            BlogId = blogId;
        }

        public ValidationResult ValidationResult { get; }
        public Guid BlogId { get; }
    }

    public class CreateBlogRequestValidator : AbstractValidator<CreateBlogRequest>
    {
        public CreateBlogRequestValidator()
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

            RuleFor(x => x.PostsPerPage)
                .Must(x => x > 0 && x < int.MaxValue)
                .WithMessage($"PostsPerPage should be between 1 and {int.MaxValue}.");

            RuleFor(x => x.DaysToComment)
                .Must(x => x > 0 && x < 365)
                .WithMessage("PostsPerPage should be between 1 and 365.");
        }
    }
}