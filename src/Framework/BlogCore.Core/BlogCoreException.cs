using System;

namespace BlogCore.Core
{
    public class BlogCoreException : Exception
    {
        public BlogCoreException(string message)
            : this(message, null)
        {
        }

        public BlogCoreException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }
    }

    public class BlogCoreDomainException : BlogCoreException
    {
        public BlogCoreDomainException(string message, Exception innerEx) 
            : base(message, innerEx)
        {
        }
    }

    public class DomainValidationException : BlogCoreException
    {
        public DomainValidationException(string message) 
            : this(message, null)
        {
        }

        public DomainValidationException(string message, Exception innerEx) 
            : base(message, innerEx)
        {
        }
    }
}