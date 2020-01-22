using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Application.Events;
using Common.Domain.Entities;
using Common.Domain.Repositories;
using Marten;

namespace Library.MartenEventStore.Repositories
{
    public class MartenEventStoreRepository<T> : IRepository<T> where T : Aggregate, new()
    {
        private readonly IEventBus _eventBus;
        private readonly IDocumentSession _session;

        public MartenEventStoreRepository(IDocumentSession session, IEventBus eventBus)
        {
            _session = session;
            _eventBus = eventBus;
        }

        public async Task<T> GetById(Guid id)
        {
            var aggregate = await _session.Events.AggregateStreamAsync<T>(id);
            return aggregate ?? throw new InvalidOperationException($"No aggregate by id {id}.");
        }

        public async Task Save(T aggregate)
        {
            dynamic events = aggregate.Events.ToArray();

            _session.Events.Append(aggregate.Id, events);
            _session.SaveChanges();

            foreach (var @event in events)
            {
                await _eventBus.Publish(@event);
            }

            aggregate.ClearEvents();
        }
    }
}