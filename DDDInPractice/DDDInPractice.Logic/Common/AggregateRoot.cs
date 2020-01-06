﻿using System.Collections.Generic;

namespace DDDInPractice.Logic.Common
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        protected virtual void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        public virtual void ClearEvents() => _domainEvents.Clear();
    }
}
