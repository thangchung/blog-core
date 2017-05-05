using System;
using System.Collections.Generic;

namespace BlogCore.Core.Posts
{
    public class Post : EntityBase
    {
        public string Name { get; set; }
        public Guid BlogId { get; set; }
        public List<Comment> Comments { get; set; }
    }
}