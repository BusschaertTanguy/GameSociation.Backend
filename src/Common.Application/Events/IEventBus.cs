using System.Threading.Tasks;
using Common.Domain.Events;

namespace Common.Application.Events
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}