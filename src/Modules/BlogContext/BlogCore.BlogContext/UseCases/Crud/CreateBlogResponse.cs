using System;
using FluentValidation.Results;

namespace BlogCore.BlogContext.UseCases.Crud
{
    public class CreateBlogResponse
    {
        public CreateBlogResponse(
            Guid blogId, 
            ValidationResult validationResult)
        {
            BlogId = blogId;
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get; }
        public Guid BlogId { get; }
    }
}