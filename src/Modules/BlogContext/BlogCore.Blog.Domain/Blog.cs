using System;
using System.ComponentModel.DataAnnotations;
using BlogCore.Core;

namespace BlogCore.Blog.Domain
{
    public class Blog : EntityBase
    {
        private Blog()
        {
        }

        public Blog(string title, string ownerEmail) 
            : this(IdHelper.GenerateId(), title, ownerEmail)
        {
        }

        public Blog(Guid id, string title, string ownerEmail) : base(id)
        {
            AssertTitle(title);
            AssertOwnerEmail(ownerEmail);

            Title = title;
            OwnerEmail = ownerEmail;
            Theme = Theme.Default;
            ImageFilePath = "/images/default-blog.png";

            Status = BlogStatus.Registered;
            Events.Add(new BlogCreated());
        }

        [Required]
        public string Title { get; private set; }

        [Required]
        public Theme Theme { get; private set; }

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

        public Blog ChangeTitle(string title)
        {
            AssertTitle(title);
            Title = title;
            return this;
        }

        public Blog ChangeTheme(Theme theme)
        {
            Theme = theme;
            return this;
        }

        public Blog ChangeImageFilePath(string imageFilePath)
        {
            AssertImageFilePath(imageFilePath);
            ImageFilePath = imageFilePath;
            return this;
        }

        public Blog ChangeSetting(BlogSetting setting)
        {
            AssertSetting(setting);
            BlogSetting = setting;
            Events.Add(new BlogSettingChanged(setting.Id));
            return this;
        }

        public Blog ChangeDescription(string description)
        {
            Description = description;
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

        private static void AssertTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new DomainValidationException("Title could not be null or empty.");
            }
        }

        private static void AssertImageFilePath(string imageFilePath)
        {
            if (string.IsNullOrEmpty(imageFilePath))
            {
                throw new DomainValidationException("The path of image could not be null or empty.");
            }
        }

        private static void AssertOwnerEmail(string ownerEmail)
        {
            if (string.IsNullOrEmpty(ownerEmail))
            {
                throw new DomainValidationException("The email of owner could not be null or empty.");
            }
        }

        private static void AssertSetting(BlogSetting setting)
        {
            if (setting == null)
            {
                throw new DomainValidationException("BlogSetting could not be null or empty.");
            }

            if (setting.PostsPerPage <= 0 || setting.PostsPerPage >= 20)
            {
                throw new DomainValidationException("PostsPerPage in BlogSetting could not be less than zero and greater than 20 posts.");
            }

            if (setting.DaysToComment <= 0 || setting.DaysToComment >= 10)
            {
                throw new DomainValidationException("PostsPerPage in BlogSetting could not be less than zero and greater than 10 days.");
            }
        }
    }
}