using System;
using System.Collections.Generic;

namespace BlogCore.Modules.PostContext.Domain
{
    public interface ITagService
    {
        IEnumerable<Tag> UpsertTags(Guid postId, IEnumerable<string> tags);
    }
}