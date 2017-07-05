using System.Collections.Generic;
using System.Linq;
using BlogCore.Core;

namespace BlogCore.Blog.Domain
{
    public class Blog : EntityBase
    {
        public Blog()
        {
            Status = BlogStatus.Registered;
            Events.Add(new BlogCreatedEvent());
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public string ImageFilePath { get; set; }
        public string OwnerEmail { get; set; }
        public BlogStatus Status { get; private set; }
        public BlogSetting BlogSetting { get; set; }
        public List<PostId> Posts { get; set; }

        public bool HasPost()
        {
            return Posts?.Any() ?? false;
        }

        public void Deactivate()
        {
            if (Status == BlogStatus.Registered || Status == BlogStatus.Activated)
            {
                Status = BlogStatus.DeActivated;
                Events.Add(new BlogStatusChangedEvent(Id, BlogStatus.DeActivated));
            }
            else
            {
                throw new BlogActivatedException("Blog has already deactivated.");
            }
        }

        public void Activate()
        {
            if (Status == BlogStatus.Registered || Status == BlogStatus.DeActivated)
            {
                Status = BlogStatus.Activated;
                Events.Add(new BlogStatusChangedEvent(Id, BlogStatus.Activated));
            }
            else
            {
                throw new BlogActivatedException("Blog has already activated.");
            }
        }
    }
}