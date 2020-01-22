using System;
using MediatR;

namespace Common.Domain.Events
{
    public interface IEvent : INotification
    {
        Guid Id { get; }
    }
}