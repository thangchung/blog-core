using System.Collections.Generic;
using System.Linq;
using BlogCore.Core;

namespace BlogCore.Blog.Domain
{
    public class Blog : EntityBase
    {
        public Blog()
        {
            Events.Add(new BlogCreatedEvent());
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public string ImageFilePath { get; set; }
        public string OwnerEmail { get; set; }
        public bool InActive { get; private set; }
        public BlogSetting BlogSetting { get; set; }
        public List<PostId> Posts { get; set; }
        public bool HasPost => Posts?.Any() ?? false;

        public void Deactivate()
        {
            InActive = true;
        }

        public void Activate()
        {
            InActive = false;
        }
    }
}