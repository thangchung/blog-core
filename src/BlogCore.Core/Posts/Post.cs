using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogCore.Core.Posts
{
    public class Post : EntityBase
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Excerpt { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public Guid BlogId { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Guid> TagIds { get; set; }

        public bool HasComments => Comments?.Any() ?? false;
    }
}