using System;
using System.Collections.Generic;
using BlogCore.Core;
using BlogCore.AccessControl.Domain;

namespace BlogCore.Post.UseCases.ListOutPostByBlog
{
    /*public class ListOutPostByBlogResponse : IMessage
    {
        public int Page { get; set; }
        public int Total { get; set; }
        public List<InnerListOutPostByBlogResponse> Inners { get; set; }
    } */

    public class ListOutPostByBlogResponse : IMessage
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Slug { get; set; }
        public ListOutPostByBlogUserResponse Author { get; set; }
        public List<ListOutPostByBlogTagResponse> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ListOutPostByBlogUserResponse
    {
        public ListOutPostByBlogUserResponse(AppUser user)
        {
            Id = IdHelper.GenerateId(user.Id);
            FamilyName = user.FamilyName;
            GivenName = user.GivenName;
        }

        public Guid Id { get; private set; }
        public string FamilyName { get; private set; }
        public string GivenName { get; private set; }
    }

    public class ListOutPostByBlogTagResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}