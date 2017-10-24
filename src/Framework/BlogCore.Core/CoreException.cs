using FluentValidation.Results;
using System;
using System.Collections.Generic;

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
        private readonly List<ValidationFailure> _failures;

        public ValidationException(string message) 
            : this(message, null)
        {
        }

        public ValidationException(string message, List<ValidationFailure> failures)
            : this(message, failures, null)
        {

        }

        public ValidationException(string message, List<ValidationFailure> failures, Exception innerEx) 
            : base(message, innerEx)
        {
            _failures = failures;
        }
    }

    public class ViolateSecurityException : CoreException
    {
        public ViolateSecurityException(string message)
            : base(message, null)
        {
        }
    }
}