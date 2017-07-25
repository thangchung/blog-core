using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BlogCore.Core;

namespace BlogCore.Post.Domain
{
    public class Post : EntityBase
    {
        internal Post()
        {
        }

        internal Post(BlogId blogId, string title, string excerpt, string body, AuthorId authorId)
            : this(blogId, IdHelper.GenerateId(), title, excerpt, body, authorId)
        {
        }

        internal Post(BlogId blogId, Guid postId, string title, string excerpt, string body, AuthorId authorId) 
            : base(postId)
        {
            Blog = blogId;
            Title = title;
            Excerpt = excerpt;
            Slug = title.GenerateSlug();
            Body = body;
            Author = authorId;
            CreatedAt = DateTimeHelper.GenerateDateTime();
            Events.Add(new PostedCreated(postId));
        }

        public static Post CreateInstance(BlogId blogId, Guid postId, string title, string excerpt, string body, AuthorId authorId)
        {
            return new Post(blogId, postId, title, excerpt, body, authorId);
        }

        public static Post CreateInstance(BlogId blogId, string title, string excerpt, string body, AuthorId authorId)
        {
            return new Post(blogId, title, excerpt, body, authorId);
        }

        /// <summary>
        /// The title of the post
        /// </summary>
        [Required]
        public string Title { get; private set; }

        /// <summary>
        /// A brief description of the post. 
        /// This description may be used by search engines, 
        /// and will be used when the post is shared on Facebook and Twitter.
        /// </summary>
        [Required]
        public string Excerpt { get; private set; }

        /// <summary>
        /// the title is used as the basis for creating a friendly ID or slug for the page. 
        /// (ex. "Welcome to our site" would be assigned a slug of "welcome-to-our-site").
        /// </summary>
        [Required]
        public string Slug { get; private set; }

        /// <summary>
        /// The body of the post
        /// </summary>
        [Required]
        public string Body { get; private set; }

        /// <summary>
        /// The blog that associates to the post
        /// </summary>
        [Required]
        public BlogId Blog { get; private set; }

        /// <summary>
        ///  The list of comments to attach to the post
        /// </summary>
        public ICollection<Comment> Comments { get; private set; } = new HashSet<Comment>();

        /// <summary>
        /// The list of tags to attach to the post
        /// </summary>
        public ICollection<Tag> Tags { get; private set; } = new HashSet<Tag>();

        /// <summary>
        /// The author that associates to the post
        /// </summary>
        [Required]
        public AuthorId Author { get; private set; }

        /// <summary>
        /// The created date of the post
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// The Updated data of the post
        /// </summary>
        public DateTime UpdatedAt { get; private set; }

        public Post ChangeTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new DomainValidationException("Title could not be null or empty.");
            }

            Title = title;
            Slug = title.GenerateSlug();
            return this;
        }

        public Post ChangeExcerpt(string excerpt)
        {
            if (string.IsNullOrEmpty(excerpt))
            {
                throw new DomainValidationException("Excerpt could not be null or empty.");
            }

            Excerpt = excerpt;
            return this;
        }

        public Post ChangeBody(string body)
        {
            if (string.IsNullOrEmpty(body))
            {
                throw new DomainValidationException("Body could not be null or empty.");
            }

            Excerpt = body;
            return this;
        }

        public bool HasComments()
        {
            return Comments?.Any() ?? false;
        }

        public Post AddComment(string body, AuthorId authorId)
        {
            Comments.Add(new Comment(body, authorId));
            return this;
        }

        public Post UpdateComment(Guid commentId, string body)
        {
            var comment = Comments.FirstOrDefault(x => x.Id == commentId);
            if (comment == null)
            {
                throw new NotFoundCommentException($"Could not find the comment with Id={commentId} for updating.");

            }
            comment.UpdateComment(body);
            return this;
        }

        public Post RemoveComment(Guid commentId)
        {
            var comment = Comments.FirstOrDefault(x => x.Id == commentId);
            if (comment == null)
            {
                throw new NotFoundCommentException($"Could not find the comment with Id={commentId} for deleting.");
                
            }
            Comments.Remove(comment);
            return this;
        }

        public bool HasTags()
        {
            return Tags?.Any() ?? false;
        }

        #region "TODO: might consider how can we manage Tags"

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
        }

        #endregion
    }
}