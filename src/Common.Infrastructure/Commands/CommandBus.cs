using System.Threading.Tasks;
using Common.Application.Commands;
using MediatR;

namespace Common.Infrastructure.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly IMediator _mediator;

        public CommandBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish<TCommand>(TCommand command) where TCommand : ICommand
        {
            await _mediator.Publish(command);
        }
    }
}