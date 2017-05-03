namespace BlogCore.Core.Blogs.ListOfBlog
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

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
    }
}