using System;
using NetCoreKit.Domain;

namespace BlogCore.Modules.BlogContext.Domain
{
    public class BlogActivatedException : CoreException
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