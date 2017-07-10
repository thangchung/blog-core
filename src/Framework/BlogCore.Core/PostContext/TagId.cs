using System;

namespace BlogCore.Core.PostContext
{
    public class TagId  : IdentityBase
    {
        private TagId()
        {
        }

        public TagId(Guid tagId) : base(tagId)
        {
        }
    }
}