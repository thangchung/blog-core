using System;
using BlogCore.Core;

namespace BlogCore.Blog.UseCases.ListOutBlog
{
    public class ListOfBlogResponse : IMesssage
    {
        public ListOfBlogResponse(
            Guid id,
            string title,
            string description,
            string image,
            string theme)
        {
            Id = id;
            Title = title;
            Description = description;
            Image = image;
            Theme = theme;
        }

        public Guid Id { get; }

        public string Title { get; }
        public string Description { get; }
        public string Image { get; }
        public string Theme { get; }
    }
}