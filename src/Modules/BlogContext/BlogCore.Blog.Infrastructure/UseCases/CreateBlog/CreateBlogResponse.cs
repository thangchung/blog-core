using System;
using BlogCore.Core;
using FluentValidation.Results;

namespace BlogCore.Blog.Infrastructure.UseCases.CreateBlog
{
    public class CreateBlogResponse : IMesssage
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