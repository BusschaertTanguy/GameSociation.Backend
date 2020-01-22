using System.Threading;
using System.Threading.Tasks;
using Account.Domain.Events;
using Association.Application.Commands.CreateAssociate;
using Common.Application.Commands;
using Common.Application.Events;

namespace Association.Application.Events
{
    public class AccountCreatedEventHandler : IEventHandler<AccountCreated>
    {
        private readonly ICommandBus _commandBus;

        public AccountCreatedEventHandler(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public async Task Handle(AccountCreated notification, CancellationToken cancellationToken)
        {
            var command = new CreateAssociate(notification.Id, notification.Username);
            await _commandBus.Publish(command).ConfigureAwait(false);
        }
    }
}