using System;

namespace BlogCore.BlogContext.UseCases.Crud
{
    public class UpdateBlogResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Theme { get; set; } = 1; //default
    }
}
