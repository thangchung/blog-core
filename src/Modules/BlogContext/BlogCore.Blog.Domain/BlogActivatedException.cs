using System;
using BlogCore.Core;

namespace BlogCore.Blog.Domain
{
    public class BlogActivatedException : BlogCoreException
    {
        public BlogActivatedException(string message)
            : this(message, null)
        {
        }

        public BlogActivatedException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }
    }
}