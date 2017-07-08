using System;

namespace BlogCore.Core
{
    public interface IIdentity
    {
        Guid Id { get; }
    }
}