using System;

namespace BlogCore.Core
{
    public interface IDateService
    {
        DateTimeOffset GetDate();
    }
}