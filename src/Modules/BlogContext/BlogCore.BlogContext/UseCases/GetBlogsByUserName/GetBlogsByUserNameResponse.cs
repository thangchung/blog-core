using System;

namespace BlogCore.BlogContext.UseCases.GetBlogsByUserName
{
    public class GetBlogsByUserNameResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set;  }
        public string Description { get; set;  }
        public string Image { get; set; }
        public int Theme { get; set; }
    }
}
