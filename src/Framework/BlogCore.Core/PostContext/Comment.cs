using System;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Core.PostContext
{
    public class Comment : EntityBase
    {
        private Comment()
        {
        }

        public Comment(string body, AuthorId authorId)
            : this(IdHelper.GenerateId(), body, authorId)
        {
        }

        public Comment(Guid postId, string body, AuthorId authorId) 
            : base(postId)
        {
            Body = body;
            AuthorId = authorId;
            CreatedAt = DateTimeHelper.GenerateDateTime();
        }

        [Required]
        public string Body { get; private set; }

        [Required]
        public AuthorId AuthorId { get; private set; }

        [Required]
        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public Comment UpdateComment(string body)
        {
            Body = body;
            UpdatedAt = DateTimeHelper.GenerateDateTime();
            return this;
        }
    }
}