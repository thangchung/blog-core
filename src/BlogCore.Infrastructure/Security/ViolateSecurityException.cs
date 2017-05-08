using BlogCore.Core;

namespace BlogCore.Infrastructure.Security
{
    public class ViolateSecurityException : BlogCoreException
    {
        public ViolateSecurityException(string message)
            : base(message, null)
        {
        }
    }
}