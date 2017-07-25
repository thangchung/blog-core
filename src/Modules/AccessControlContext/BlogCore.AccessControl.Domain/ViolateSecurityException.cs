using BlogCore.Core;

namespace BlogCore.AccessControl.Domain
{
    public class ViolateSecurityException : CoreException
    {
        public ViolateSecurityException(string message)
            : base(message, null)
        {
        }
    }
}