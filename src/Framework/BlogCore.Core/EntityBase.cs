using BlogCore.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Core
{
    public abstract class EntityBase : IIdentity
    {
        protected List<IDomainEvent> Events = new List<IDomainEvent>();

        protected EntityBase() : this(IdHelper.GenerateId())
        {            
        }

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

        public EntityBase RemoveEvent(IDomainEvent @event)
        {
            if (Events.Find(e => e == @event) != null)
            {
                Events.Remove(@event);
            }
            return this;
        }

        public EntityBase RemoveAllEvents()
        {
            Events = new List<IDomainEvent>();
            return this;
        }
    }
}