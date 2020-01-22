using MediatR;

namespace Common.Application.Commands
{
    public interface ICommandHandler<in TCommand> : INotificationHandler<TCommand> where TCommand : ICommand
    {
    }
}