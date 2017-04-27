namespace BlogCore.Core.BlogFeature
{
    public class Blog : EntityBase
    {
        public Blog()
        {
            Events.Add(new BlogCreatedEvent());
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public string Image { get; set; }
        public int PostsPerPage { get; set; }
        public int DaysToComment { get; set; }
        public bool ModerateComments { get; set; }
    }
}