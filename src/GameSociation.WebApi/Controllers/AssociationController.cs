using System;
using System.Threading.Tasks;
using Association.Application.Commands.AcceptInvitation;
using Association.Application.Commands.CreateAssociation;
using Association.Application.Commands.InviteToAssociation;
using Association.Application.Commands.RefuseInvitation;
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
        public async Task<ActionResult<AssociationDetailView>> GetAssociation(Guid id)
            => await ProcessQuery<GetAssociation, AssociationDetailView>(new GetAssociation(id));

        [HttpPost("{id}/invite")]
        public async Task<ActionResult> InviteToAssociation([FromBody] InviteToAssociation command)
            => await PublishCommand(command);

        [HttpPost("{id}/membership/{associateId}/accept")]
        public async Task<ActionResult> AcceptInvitation([FromBody] AcceptInvitation command)
            => await PublishCommand(command);

        [HttpPost("{id}/membership/{associateId}/refuse")]
        public async Task<ActionResult> RefuseInvitation([FromBody] RefuseInvitation command)
            => await PublishCommand(command);
    }
}