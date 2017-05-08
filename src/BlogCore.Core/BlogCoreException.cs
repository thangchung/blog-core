using System;

namespace BlogCore.Core
{
    public class BlogCoreException : Exception
    {
        public BlogCoreException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }
    }
}