using System;
using System.Collections.Generic;
using BlogCore.Core;

namespace BlogCore.BlogContext.Core.Domain
{
    public class BlogSetting : ValueObjectBase
    {
        private BlogSetting()
        {
        }

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

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PostsPerPage;
            yield return DaysToComment;
            yield return ModerateComments;
        }
    }
}