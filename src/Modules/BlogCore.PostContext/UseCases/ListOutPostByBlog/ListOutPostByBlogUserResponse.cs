using BlogCore.Core;
using BlogCore.Core.Helpers;
using System;

namespace BlogCore.PostContext.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogUserResponse : IMessage
    {
        public ListOutPostByBlogUserResponse(string id, string familyName, string givenName)
        {
            Id = IdHelper.GenerateId(id);
            FamilyName = familyName;
            GivenName = givenName;
        }

        public Guid Id { get; private set; }
        public string FamilyName { get; private set; }
        public string GivenName { get; private set; }
    }
}