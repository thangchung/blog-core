using System;

namespace BlogCore.Core.PostContext
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