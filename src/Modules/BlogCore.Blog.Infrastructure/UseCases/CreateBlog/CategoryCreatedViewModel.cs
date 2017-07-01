using System;
using BlogCore.Core;

namespace BlogCore.Blog.Infrastructure.UseCases.CreateBlog
{
    public class CategoryCreatedViewModel : IViewModel
    {
        public Guid BlogId { get; set; }
    }
}