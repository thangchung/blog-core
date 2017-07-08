using System;
using MediatR;

namespace BlogCore.Core
{
    public interface IDomainEvent : INotification
    {
        int EventVersion { get; set; }
        DateTime OccurredOn { get; set; }
    }
}