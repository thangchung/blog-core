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

    public class ValidationException : CoreException
    {
        public ValidationException(string message) 
            : this(message, null)
        {
        }

        public ValidationException(string message, Exception innerEx) 
            : base(message, innerEx)
        {
        }
    }
}