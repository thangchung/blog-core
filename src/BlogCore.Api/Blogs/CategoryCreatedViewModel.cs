using System;
using BlogCore.Core;

namespace BlogCore.Api.Blogs
{
    public class CategoryCreatedViewModel : IViewModel
    {
        public Guid BlogId { get; set; }
    }
}