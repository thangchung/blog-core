namespace BlogCore.Core.Blogs.ListOutBlogs
{
    public class ListOfBlogResponseMsg : IMesssage
    {
        public ListOfBlogResponseMsg(
            string title,
            string description,
            string image)
        {
            Title = title;
            Description = description;
            Image = image;
        }

        public string Title { get; }
        public string Description { get; }
        public string Image { get; }
    }
}