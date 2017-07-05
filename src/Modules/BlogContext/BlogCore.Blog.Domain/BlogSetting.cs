using System;
using BlogCore.Core;

namespace BlogCore.Blog.Domain
{
    public class BlogSetting: ValueObject
    {
        public BlogSetting(Guid id, int postsPerPage, int daysToComment, bool moderateComments)
        {
            Id = id;
            PostsPerPage = postsPerPage;
            DaysToComment = daysToComment;
            ModerateComments = moderateComments;
        }

        public Guid Id { get; private set; }
        public int PostsPerPage { get; private set; }
        public int DaysToComment { get; private set; }
        public bool ModerateComments { get; private set; }
    }
}