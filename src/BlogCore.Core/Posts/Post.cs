using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogCore.Core.Posts
{
    public class Post : EntityBase
    {
        public string Name { get; set; }
        public Guid BlogId { get; set; }
        public List<Comment> Comments { get; set; }

        public bool HasComments => Comments?.Any() ?? false;
    }
}