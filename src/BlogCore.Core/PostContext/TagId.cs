using System;

namespace BlogCore.Core.PostContext
{
    public class TagId  : ValueObject
    {
        private TagId()
        {
            
        }

        public TagId(Guid tagId)
        {
            Id = tagId;
        }

        public Guid Id { get; private set; }
    }
}