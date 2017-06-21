using System;
using BlogCore.Core;

namespace BlogCore.Api.Blogs.CreateBlog
{
    public class CategoryCreatedViewModel : IViewModel
    {
        public Guid BlogId { get; set; }
    }
}