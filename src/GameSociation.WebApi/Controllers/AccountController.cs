using System.Threading.Tasks;
using Account.Application.Commands.Register;
using Account.Application.Queries.Login;
using Common.Application.Commands;
using Common.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameSociation.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        public AccountController(IQueryBus queryBus, ICommandBus commandBus) : base(queryBus, commandBus)
        {
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register command)
            => await PublishCommand(command);

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> Login([FromBody] Login query)
            => await ProcessQuery<Login, LoginResult>(query);
    }
}