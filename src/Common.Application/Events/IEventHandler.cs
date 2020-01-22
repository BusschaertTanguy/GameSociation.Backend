using Common.Domain.Events;
using MediatR;

namespace Common.Application.Events
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
    {
    }
}