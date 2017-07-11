using System;
using BlogCore.Core;

namespace BlogCore.Post.Domain
{
    public class AuthorId : IdentityBase
    {
        private AuthorId()
        {
        }

        public AuthorId(Guid authorId) : base(authorId)
        {
        }
    }
}