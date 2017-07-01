using System;
using BlogCore.Core;

namespace BlogCore.Blog.Infrastructure.UseCases.GetBlog
{
    public class GetBlogResponse : IMesssage
    {
        public GetBlogResponse(
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