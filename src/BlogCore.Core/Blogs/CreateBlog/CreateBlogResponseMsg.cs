using System;
using FluentValidation.Results;

namespace BlogCore.Core.Blogs.CreateBlog
{
    public class CreateBlogResponseMsg : IMesssage
    {
        public CreateBlogResponseMsg(
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