using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Association.Application.Queries.GetAssociateByAccountId;
using Association.Application.Queries.GetJoinedAssociations;
using Association.Application.Queries.GetOwnedAssociations;
using Association.Application.Views;
using Common.Application.Commands;
using Common.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameSociation.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AssociateController : BaseController
    {
        public AssociateController(IQueryBus queryBus, ICommandBus commandBus) : base(queryBus, commandBus)
        {
        }

        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<AssociateView>> GetAssociateByAccountId(Guid accountId)
            => await ProcessQuery<GetAssociateByAccountId, AssociateView>(new GetAssociateByAccountId(accountId));

        [HttpGet("{id}/association/owned")]
        public async Task<ActionResult<IEnumerable<AssociationView>>> GetOwnedAssociations(Guid id)
            => await ProcessQuery<GetOwnedAssociations, IEnumerable<AssociationView>>(new GetOwnedAssociations(id));

        [HttpGet("{id}/association/joined")]
        public async Task<ActionResult<IEnumerable<AssociationView>>> GetJoinedAssociations(Guid id)
            => await ProcessQuery<GetJoinedAssociations, IEnumerable<AssociationView>>(new GetJoinedAssociations(id));
    }
}