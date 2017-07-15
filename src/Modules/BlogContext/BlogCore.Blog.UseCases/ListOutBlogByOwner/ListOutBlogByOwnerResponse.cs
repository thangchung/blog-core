using System;
using BlogCore.Core;

namespace BlogCore.Blog.UseCases.ListOutBlogByOwner
{
    public class ListOutBlogByOwnerResponse : IMesssage
    {
        public ListOutBlogByOwnerResponse(
            Guid id,
            string title,
            string description,
            string image,
            int theme)
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
        public int Theme { get; }
    }
}