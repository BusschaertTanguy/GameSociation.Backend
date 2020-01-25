using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Account.Application.Projections;
using Account.Application.Services;
using Account.Domain.Repositories;
using Common.Application.Commands;
using Common.Application.Queries;

namespace Account.Application.Commands.Register
{
    public class RegisterHandler : ICommandHandler<Register>
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAccountRepository _accountRepository;
        private readonly IHashService _hashService;

        public RegisterHandler(IQueryProcessor queryProcessor, IAccountRepository accountRepository, IHashService hashService)
        {
            _queryProcessor = queryProcessor;
            _accountRepository = accountRepository;
            _hashService = hashService;
        }

        public async Task Handle(Register notification, CancellationToken cancellationToken)
        {
            var email = new MailAddress(notification.Email);
            var emailInUse = _queryProcessor.Query<AccountProjection>().Any(x => x.Email == email.Address);

            if (emailInUse)
                throw new InvalidOperationException($"Account with email {email.Address} already exists");

            var hashedPassword = _hashService.Hash(notification.Password);
            var account = new Domain.Entities.Account(email.Address, hashedPassword, notification.Username);
            await _accountRepository.Save(account).ConfigureAwait(false);
        }
    }
}