using System;

namespace BlogCore.Core
{
    public class CoreException : Exception
    {
        public CoreException(string message)
            : this(message, null)
        {
        }

        public CoreException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }
    }

    public class BlogCoreDomainException : CoreException
    {
        public BlogCoreDomainException(string message, Exception innerEx) 
            : base(message, innerEx)
        {
        }
    }

    public class DomainValidationException : CoreException
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