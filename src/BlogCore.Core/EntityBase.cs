using System;
using System.Collections.Generic;

namespace BlogCore.Core
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public List<DomainEventBase> Events = new List<DomainEventBase>();
    }
}