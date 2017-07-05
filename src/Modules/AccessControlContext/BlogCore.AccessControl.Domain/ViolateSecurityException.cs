using BlogCore.Core;

namespace BlogCore.AccessControl.Domain
{
    public class ViolateSecurityException : BlogCoreException
    {
        public ViolateSecurityException(string message)
            : base(message, null)
        {
        }
    }
}