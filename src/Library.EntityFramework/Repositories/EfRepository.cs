using System;
using System.Threading.Tasks;
using Common.Application.Events;
using Common.Domain.Entities;
using Common.Domain.Repositories;
using Library.EntityFramework.Contexts;

namespace Library.EntityFramework.Repositories
{
    public class EfRepository<TAggregate> : IRepository<TAggregate> where TAggregate : Aggregate
    {
        private readonly IEventBus _eventBus;
        protected readonly GameSociationContext Context;

        public EfRepository(GameSociationContext context, IEventBus eventBus)
        {
            Context = context;
            _eventBus = eventBus;
        }

        public async Task<TAggregate> GetById(Guid id)
        {
            var aggregate = await Context.Set<TAggregate>().FindAsync(id).ConfigureAwait(false);
            if (aggregate == null)
                throw new InvalidOperationException($"No aggregate found of type {nameof(TAggregate)} with id {id}");
            return aggregate;
        }

        public async Task Save(TAggregate aggregate)
        {
            var existingAggregate = await Context.Set<TAggregate>().FindAsync(aggregate.Id).ConfigureAwait(false);

            if (existingAggregate == null) await Context.Set<TAggregate>().AddAsync(aggregate).ConfigureAwait(false);
            else Context.Update(aggregate);

            await Context.SaveChangesAsync();

            foreach (dynamic aggregateEvent in aggregate.Events)
            {
                await _eventBus.Publish(aggregateEvent).ConfigureAwait(false);
            }

            aggregate.ClearEvents();
        }
    }
}