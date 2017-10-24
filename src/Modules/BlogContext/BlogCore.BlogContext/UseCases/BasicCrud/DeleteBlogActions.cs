using BlogCore.Infrastructure.UseCase;
using FluentValidation;
using System;

namespace BlogCore.BlogContext.UseCases.BasicCrud
{
    public class DeleteBlogRequest : IRequest<DeleteBlogResponse>
    {
        public Guid Id { get; set; }
    }

    public class DeleteBlogResponse
    {
        public DeleteBlogResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public class DeleteBlogRequestValidator : AbstractValidator<DeleteBlogRequest>
    {
        public DeleteBlogRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Id should not be null.");
        }
    }
}
