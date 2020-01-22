using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Common.Domain.Events;

namespace Common.Domain.Entities
{
    public abstract class Aggregate : Entity
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        [JsonIgnore] public IReadOnlyCollection<IEvent> Events => _events.AsReadOnly();

        protected void RaiseEvent<TEvent>(TEvent @event, Action<TEvent> apply) where TEvent : IEvent
        {
            _events.Add(@event);
            apply(@event);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}