using System;
using System.Collections.Generic;
using BlogCore.Core;
using BlogCore.Post.Domain;

namespace BlogCore.Post.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogResponse : IMesssage
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Slug { get; set; }
        public AuthorId Author { get; set; }
        public List<Tag> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}