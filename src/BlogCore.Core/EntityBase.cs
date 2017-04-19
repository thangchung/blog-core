using System.Collections.Generic;

namespace BlogCore.Core
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public List<DomainEventBase> Events = new List<DomainEventBase>();
    }
}