using BlogCore.Core;
using System;

namespace BlogCore.PostContext.Domain
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