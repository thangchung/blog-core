using BlogCore.Shared.v1.Blog;
using NetCoreKit.Domain;
using NetCoreKit.Utils.Extensions;
using NetCoreKit.Utils.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Modules.BlogContext.Domain
{
    public class Blog : AggregateRootWithIdBase<Guid>
    {
        private Blog()
        {
            AddEvent(new BlogCreated());
        }

        public static Blog Of(CreateBlogRequest request)
        {
            var blog = new Blog
            {
                Title = request.Title,
                OwnerEmail = request.OwnerEmail,
                Description = request.Description,
                Theme = Theme.Default,
                Status = BlogStatus.Registered,
                ImageFilePath = "/images/default-blog.png"
            };

            if (request.BlogSetting != null && request.BlogSetting.Id == Guid.Empty.ToString())
            {
                blog.BlogSetting = new BlogSetting(
                    IdHelper.GenerateId(),
                    request.BlogSetting.PostsPerPage,
                    request.BlogSetting.DaysToComment,
                    request.BlogSetting.ModerateComments);
            }
            else
            {
                blog.ChangeSetting(new BlogSetting(
                    request.BlogSetting.Id.ConvertTo<Guid>(),
                    request.BlogSetting.PostsPerPage,
                    request.BlogSetting.DaysToComment,
                    request.BlogSetting.ModerateComments));
            }

            return blog;
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

        public Blog ChangeSetting(BlogSetting setting)
        {
            Guid oldBlogSettingId = Guid.Empty;
            if (BlogSetting != null)
            {
                oldBlogSettingId = BlogSetting.BlogSettingId;
            }

            BlogSetting = setting;
            if (BlogSetting != null && Guid.Empty != oldBlogSettingId)
            {
                AddEvent(new BlogSettingChanged(oldBlogSettingId));
            }
            return this;
        }

        public Blog Deactivate()
        {
            if (Status == BlogStatus.Registered || Status == BlogStatus.Activated)
            {
                Status = BlogStatus.Deactivated;
                AddEvent(new BlogStatusChanged(Id, BlogStatus.Deactivated));
            }
            else
            {
                throw new BlogActivatedException("Blog has already deactivated.");
            }
            return this;
        }

        public Blog Activate()
        {
            if (Status == BlogStatus.Registered || Status == BlogStatus.Deactivated)
            {
                Status = BlogStatus.Activated;
                AddEvent(new BlogStatusChanged(Id, BlogStatus.Activated));
            }
            else
            {
                throw new BlogActivatedException("Blog has already activated.");
            }
            return this;
        }
    }
}