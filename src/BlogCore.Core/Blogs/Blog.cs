using System.Collections.Generic;
using System.Linq;
using BlogCore.Core.Blogs.CreateBlog;
using BlogCore.Core.Posts;

namespace BlogCore.Core.Blogs
{
    public class Blog : EntityBase
    {
        public Blog()
        {
            Events.Add(new BlogCreatedEvent());
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public string Image { get; set; }
        public int PostsPerPage { get; set; }
        public int DaysToComment { get; set; }
        public bool ModerateComments { get; set; }
        public string OwnerEmail { get; set; }

        public List<Post> Posts { get; set; }

        public bool HasPost => Posts?.Any() ?? false;
    }
}