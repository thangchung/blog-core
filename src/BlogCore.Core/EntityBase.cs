using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Core
{
    public abstract class EntityBase : IIdentity
    {
        protected List<IDomainEvent> Events = new List<IDomainEvent>();

        protected EntityBase(Guid id)
        {
            Id = id;
        }

        [Key]
        public Guid Id { get; protected set; }

        public List<IDomainEvent> GetEvents()
        {
            return Events;
        }
    }
}