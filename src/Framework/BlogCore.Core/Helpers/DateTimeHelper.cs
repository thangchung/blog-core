using System;

namespace BlogCore.Core.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GenerateDateTime()
        {
            return DateTimeOffset.Now.UtcDateTime;
        }
    }
}