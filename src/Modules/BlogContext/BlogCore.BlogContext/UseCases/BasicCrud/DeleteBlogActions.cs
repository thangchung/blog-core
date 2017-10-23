using BlogCore.Infrastructure.UseCase;
using FluentValidation;
using System;

namespace BlogCore.BlogContext.UseCases.BasicCrud
{
    public interface IDeleteItemWithIdentity
    {
        Guid Id { get; set; }
    }

    public class DeleteBlogRequest : IRequest<DeleteBlogResponse>, IDeleteItemWithIdentity
    {
        public Guid Id { get; set; }
    }

    public class DeleteBlogResponse
    {
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
