using System;
using BlogCore.Core;
using FluentValidation.Results;

namespace BlogCore.Blog.UseCases.CreateBlog
{
    public class CreateBlogResponse : IMessage
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