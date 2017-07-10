using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlogCore.Core.PostContext
{
    public class Post : EntityBase
    {
        private Post()
        {
        }

        public Post(BlogId blogId, string title, string excerpt, string body, AuthorId authorId)
            : this(blogId, IdHelper.GenerateId(), title, excerpt, body, authorId)
        {
        }

        public Post(BlogId blogId, Guid postId, string title, string excerpt, string body, AuthorId authorId) 
            : base(postId)
        {
            BlogId = blogId;
            Title = title;
            Excerpt = excerpt;
            Slug = title.GenerateSlug();
            Body = body;
            AuthorId = authorId;
            CreatedAt = DateTimeHelper.GenerateDateTime();
            Events.Add(new PostedCreated(postId));
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
        public BlogId BlogId { get; private set; }

        /// <summary>
        ///  The list of comments to attach to the post
        /// </summary>
        public List<Comment> Comments { get; set; } = new List<Comment>();

        /// <summary>
        /// The list of tags to attach to the post
        /// </summary>
        public List<TagId> TagIds { get; private set; } = new List<TagId>();

        /// <summary>
        /// The author that associates to the post
        /// </summary>
        [Required]
        public AuthorId AuthorId { get; private set; }

        [Required]
        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

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
            var comment = Comments.Find(x => x.Id == commentId);
            if (comment == null)
            {
                throw new NotFoundCommentException($"Could not find the comment with Id={commentId} for updating.");

            }
            comment.UpdateComment(body);
            return this;
        }

        public Post RemoveComment(Guid commentId)
        {
            var comment = Comments.Find(x => x.Id == commentId);
            if (comment == null)
            {
                throw new NotFoundCommentException($"Could not find the comment with Id={commentId} for deleting.");
                
            }
            Comments.Remove(comment);
            return this;
        }

        public bool HasTags()
        {
            return TagIds?.Any() ?? false;
        }

        public Post AssignTag(TagId tagId)
        {
            TagIds.Add(tagId);
            return this;       
        }

        public Post RemoveTag(TagId tagId)
        {
            TagIds.Remove(tagId);
            return this;    
        }
    }
}