using System.Threading.Tasks;
using Common.Application.Events;
using Common.Domain.Events;
using MediatR;

namespace Common.Infrastructure.Events
{
    public class EventBus : IEventBus
    {
        private readonly IMediator _mediator;

        public EventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            await _mediator.Publish(@event).ConfigureAwait(false);
        }
    }
}