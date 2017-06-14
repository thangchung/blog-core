using System;

namespace BlogCore.Core.Blogs.GetBlog
{
    public class GetBlogResponseMsg : IMesssage
    {
        public GetBlogResponseMsg(
            Guid id, 
            string title, 
            string description, 
            string theme, 
            string image)
        {
            Id = id;
            Title = title;
            Description = description;
            Theme = theme;
            Image = image;
        }

        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public string Theme { get; }
        public string Image { get; }
    }
}