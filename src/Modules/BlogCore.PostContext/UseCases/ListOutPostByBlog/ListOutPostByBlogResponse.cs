using BlogCore.Core;
using System;
using System.Collections.Generic;

namespace BlogCore.PostContext.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogResponse : IMessage
    {
        public ListOutPostByBlogResponse(
            Guid id, 
            string title, 
            string excerpt, 
            string slug, 
            DateTime createdAt, 
            ListOutPostByBlogUserResponse author, 
            List<ListOutPostByBlogTagResponse> tags)
        {
            Id = id;
            Title = title;
            Excerpt = excerpt;
            Slug = slug;
            CreatedAt = createdAt;
            Author = author;
            Tags = tags;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Excerpt { get; private set; }
        public string Slug { get; private set; }
        public ListOutPostByBlogUserResponse Author { get; private set; }
        public List<ListOutPostByBlogTagResponse> Tags { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ListOutPostByBlogResponse SetAuthor(string id, string familyName, string givenName)
        {
            Author = new ListOutPostByBlogUserResponse(id, familyName, givenName);
            return this;
        }
    }
}