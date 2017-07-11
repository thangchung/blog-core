using System;
using BlogCore.Core;

namespace BlogCore.Blog.UseCases.GetBlog
{
    public class GetBlogResponse : IMesssage
    {
        public GetBlogResponse(
            Guid id, 
            string title, 
            string description, 
            int theme, 
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
        public int Theme { get; }
        public string Image { get; }
    }
}