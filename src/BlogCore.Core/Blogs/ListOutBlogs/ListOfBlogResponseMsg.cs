using System;

namespace BlogCore.Core.Blogs.ListOutBlogs
{
    public class ListOfBlogResponseMsg : IMesssage
    {
        public ListOfBlogResponseMsg(
            Guid id,
            string title,
            string description,
            string image)
        {
            Id = id;
            Title = title;
            Description = description;
            Image = image;
        }

        public Guid Id { get; }

        public string Title { get; }
        public string Description { get; }
        public string Image { get; }
    }
}