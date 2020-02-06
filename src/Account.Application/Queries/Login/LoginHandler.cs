using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Account.Application.Exceptions;
using Account.Application.Projections;
using Account.Application.Services;
using Common.Application.Queries;

namespace Account.Application.Queries.Login
{
    public class LoginHandler : IQueryHandler<Login, LoginResult>
    {
        private readonly IHashService _hashService;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ITokenService _tokenService;

        public LoginHandler(IQueryProcessor queryProcessor, IHashService hashService, ITokenService tokenService)
        {
            _queryProcessor = queryProcessor;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        public Task<LoginResult> Handle(Login request, CancellationToken cancellationToken)
        {
            var email = new MailAddress(request.Email);

            var account = _queryProcessor.Query<AccountProjection>().FirstOrDefault(x => x.Email == email.Address);
            if (account == null)
                throw new InvalidOperationException($"Account with email {request.Email} doesn't exists");

            var passwordMatches = _hashService.Compare(account.Password, request.Password);
            if (!passwordMatches)
                throw new UnauthorizedException("Password doesn't match");

            var token = _tokenService.Generate(email.Address);
            return Task.FromResult(new LoginResult(account.Id, token));
        }
    }
}