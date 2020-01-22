using System.Threading.Tasks;

namespace Common.Application.Commands
{
    public interface ICommandBus
    {
        Task Publish<TCommand>(TCommand command) where TCommand : ICommand;
    }
}