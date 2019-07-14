using System;

namespace BlogCore.Shared.v1.Guard
{
    public static class GuardExtensions
    {
        public static TReturn NotNull<TReturn>(this TReturn value, string message = "")
        {
            if(value == null)
                throw new NullReferenceException(message);
            return value;
        }
    }
}
