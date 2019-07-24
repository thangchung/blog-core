using BlogCore.Shared;
using BlogCore.Shared.v1.Post;
using NetCoreKit.Domain;
using NetCoreKit.Utils.Extensions;
using NetCoreKit.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlogCore.Modules.PostContext.Domain
{
    public class Post : AggregateRootWithIdBase<Guid>
    {
        private Post()
        {
        }

        public static Post CreateInstance(CreatePostRequest request)
        {
            return new Post
            {
                Blog = new BlogId(request.BlogId.ConvertTo<Guid>()),
                Id = IdHelper.GenerateId(),
                Title = request.Title,
                Excerpt = request.Excerpt,
                Body = request.Body,
                Author = new AuthorId(request.AuthorId.ConvertTo<Guid>())
            };
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

        public Post UpdatePost(UpdatePostRequest request, ITagService tagService)
        {
            Title = request.Title;
            Slug = request.Title.GenerateSlug();
            Body = request.Body;
            Excerpt = request.Excerpt;
            Tags = tagService.UpsertTags(request.Id.ConvertTo<Guid>(), request.Tags).ToList();
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
    }
}