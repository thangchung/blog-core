using BlogCore.Core;

namespace BlogCore.Api.Blogs
{
    public class CategoryCreatedViewModel : IViewModel
    {
        public long? BlogId { get; set; }
    }
}