using BlogCore.Modules.PostContext.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogCore.Modules.PostContext.Services
{
    public class TagService : ITagService
    {
        public IEnumerable<Tag> UpsertTags(Guid postId, IEnumerable<string> tags)
        {
            /*
            public Post AssignTag(string name)
            {
                var tag = Tags.FirstOrDefault(x => x.Name == name);
                if (tag == null)
                {
                    Tags.Add(new Tag(IdHelper.GenerateId(), name, 1));
                }
                else
                {
                    tag.IncreaseFrequency();
                }
                return this;
            }

            public Post RemoveTag(string name)
            {
                var tag = Tags.FirstOrDefault(x => x.Name == name);
                if (tag != null)
                {
                    tag.DecreaseFrequency();
                    Tags.Remove(tag);
                }
                return this;
            }*/

            // TODO: 1. upsert tag; 2. link with Post
            return tags.Select(t => Tag.Of(new Shared.v1.Post.CreateTagRequest { Name = t, Frequency = 1 }));
        }
    }
}
