using System;
using System.Collections.Generic;
using BlogCore.Core;

namespace BlogCore.Post.Presenters.ListOutPostByBlog
{
    public class SimplePostViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public AuthorViewModel Author { get; set; }
        public List<TagViewModel> Tags { get; set; }
    }

    public class PostViewModel : IViewModel
    {
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
    }

    public class AuthorViewModel
    {
        public Guid Id { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
    }

    public class TagViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}