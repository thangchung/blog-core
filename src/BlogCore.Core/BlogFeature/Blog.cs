namespace BlogCore.Core.BlogFeature
{
    public class Blog : EntityBase
    {
        public Blog(string title, string description, string theme, string image, int postsPerPage, int daysToComment, bool moderateComments)
        {
            Title = title;
            Description = description;
            Theme = theme;
            Image = image;
            PostsPerPage = postsPerPage;
            DaysToComment = daysToComment;
            ModerateComments = moderateComments;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Theme { get; private set; }
        public string Image { get; private set; }
        public int PostsPerPage { get; private set; }
        public int DaysToComment { get; private set; }
        public bool ModerateComments { get; private set; }
    }
}