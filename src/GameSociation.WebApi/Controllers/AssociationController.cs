using System;
using System.Threading.Tasks;
using Association.Application.Commands.CreateAssociation;
using Association.Application.Commands.InviteToAssociation;
using Association.Application.Queries.GetAssociation;
using Association.Application.Views;
using Common.Application.Commands;
using Common.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameSociation.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AssociationController : BaseController
    {
        public AssociationController(IQueryBus queryBus, ICommandBus commandBus) : base(queryBus, commandBus)
        {
        }

        [HttpPost]
        public async Task<ActionResult> CreateAssociation([FromBody] CreateAssociation command)
            => await PublishCommand(command);

        [HttpGet("{id}")]
        public async Task<ActionResult<AssociationView>> GetAssociation(Guid id)
            => await ProcessQuery<GetAssociation, AssociationView>(new GetAssociation(id));

        [HttpPost("{id}/invite")]
        public async Task<ActionResult> InviteToAssociation([FromBody] InviteToAssociation command)
            => await PublishCommand(command);
    }
}