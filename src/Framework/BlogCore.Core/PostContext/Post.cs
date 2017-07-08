using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogCore.Core.PostContext
{
    public class Post : EntityBase
    {
        public Post(Guid id) : base(id)
        {
        }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Excerpt { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public BlogId BlogId { get; set; }
        public List<Comment> Comments { get; set; }
        public List<TagId> TagIds { get; set; }

        public bool HasComments => Comments?.Any() ?? false;
    }
}