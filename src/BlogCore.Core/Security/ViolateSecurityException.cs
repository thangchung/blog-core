namespace BlogCore.Core.Security
{
    public class ViolateSecurityException : BlogCoreException
    {
        public ViolateSecurityException(string message)
            : base(message, null)
        {
        }
    }
}