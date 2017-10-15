using BlogCore.Core;
using System;

namespace BlogCore.PostContext.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogTagResponse : IMessage
    {
        public ListOutPostByBlogTagResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}