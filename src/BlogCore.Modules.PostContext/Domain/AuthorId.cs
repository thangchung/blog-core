using NetCoreKit.Domain;
using System;

namespace BlogCore.Modules.PostContext.Domain
{
    public class AuthorId : IdentityBase<Guid>
    {
        public AuthorId(Guid authorId) : base(authorId)
        {
        }
    }
}