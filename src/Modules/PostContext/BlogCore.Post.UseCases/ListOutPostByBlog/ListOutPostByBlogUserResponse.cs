using System;
using BlogCore.Core;
using BlogCore.AccessControl.Domain;

namespace BlogCore.Post.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogUserResponse : IMessage
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
}