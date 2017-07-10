using System;

namespace BlogCore.Core
{
    public static class DateTimeHelper
    {
        public static DateTime GenerateDateTime()
        {
            return DateTimeOffset.Now.UtcDateTime;
        }
    }
}