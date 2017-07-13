using System;
using BlogCore.Core;

namespace BlogCore.Blog.Presenters.CreateBlog
{
    public class CategoryCreatedViewModel : IViewModel
    {
        public Guid BlogId { get; set; }
    }
}