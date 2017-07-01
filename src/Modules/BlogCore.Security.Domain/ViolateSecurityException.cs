using BlogCore.Core;

namespace BlogCore.Security.Domain
{
    public class ViolateSecurityException : BlogCoreException
    {
        public ViolateSecurityException(string message)
            : base(message, null)
        {
        }
    }
}