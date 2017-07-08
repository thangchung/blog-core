using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BlogCore.Core;

namespace BlogCore.Blog.Domain
{
    public class Blog : EntityBase
    {
        private Blog() : base(Guid.NewGuid())
        {
            
        }

        public Blog(string title, string ownerEmail) 
            : this(Guid.NewGuid(), title, ownerEmail)
        {
        }

        public Blog(Guid id, string title, string ownerEmail) : base(id)
        {
            Title = title;
            OwnerEmail = ownerEmail;
            Theme = "default";
            ImageFilePath = "/images/default-blog.png";

            Status = BlogStatus.Registered;
            Events.Add(new BlogCreated());
        }

        [Required]
        public string Title { get; private set; }

        [Required]
        public string Theme { get; private set; }

        [Required]
        public string ImageFilePath { get; private set; }

        [Required]
        [EmailAddress]
        public string OwnerEmail { get; private set; }

        [Required]
        public BlogStatus Status { get; private set; }

        [Required]
        public BlogSetting BlogSetting { get; private set; }

        public string Description { get; private set; }
        public List<PostId> Posts { get; private set; } = new List<PostId>();

        public bool HasPost()
        {
            return Posts?.Any() ?? false;
        }

        public Blog AddBlogPost(PostId postId)
        {
            Posts.Add(postId);
            return this;
        }

        public Blog RemoveBlogPost(PostId postId)
        {
            Posts.Remove(postId);
            return this;
        }

        public Blog UpdateDescription(string description)
        {
            Description = description;
            return this;
        }

        public Blog UpdateBlogSetting(BlogSetting setting)
        {
            BlogSetting = setting;
            Events.Add(new BlogSettingChanged(setting.Id));
            return this;
        }

        public Blog Deactivate()
        {
            if (Status == BlogStatus.Registered || Status == BlogStatus.Activated)
            {
                Status = BlogStatus.DeActivated;
                Events.Add(new BlogStatusChanged(Id, BlogStatus.DeActivated));
            }
            else
            {
                throw new BlogActivatedException("Blog has already deactivated.");
            }
            return this;
        }

        public Blog Activate()
        {
            if (Status == BlogStatus.Registered || Status == BlogStatus.DeActivated)
            {
                Status = BlogStatus.Activated;
                Events.Add(new BlogStatusChanged(Id, BlogStatus.Activated));
            }
            else
            {
                throw new BlogActivatedException("Blog has already activated.");
            }
            return this;
        }
    }
}