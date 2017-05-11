using System;

namespace BlogCore.Core.Blogs.GetBlog
{
    public class GetBlogResponseMsg : IMesssage
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public string Image { get; set; }
    }
}