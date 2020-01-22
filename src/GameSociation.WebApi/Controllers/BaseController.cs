using System.Threading.Tasks;
using Common.Application.Commands;
using Common.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GameSociation.WebApi.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        protected BaseController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        protected async Task<ActionResult> PublishCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            await _commandBus.Publish(command);
            return NoContent();
        }

        protected async Task<ActionResult<TResult>> ProcessQuery<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var result = await _queryBus.Process<TQuery, TResult>(query);
            return Ok(result);
        }
    }
}
