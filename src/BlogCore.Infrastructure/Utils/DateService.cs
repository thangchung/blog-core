using System;
using BlogCore.Core;

namespace BlogCore.Infrastructure.Utils
{
    public class DateService : IDateService
    {
        public DateTimeOffset GetDate()
        {
            return DateTimeOffset.Now;
        }
    }
}