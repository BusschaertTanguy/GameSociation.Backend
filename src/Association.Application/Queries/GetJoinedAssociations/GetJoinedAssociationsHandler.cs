using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Association.Application.Views;
using Association.Domain.Enumerations;
using Common.Application.Queries;

namespace Association.Application.Queries.GetJoinedAssociations
{
    public class GetJoinedAssociationsHandler : IQueryHandler<GetJoinedAssociations, IEnumerable<AssociationView>>
    {
        private readonly IQueryProcessor _queryProcessor;

        public GetJoinedAssociationsHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<IEnumerable<AssociationView>> Handle(GetJoinedAssociations request, CancellationToken cancellationToken)
        {
            var associations = _queryProcessor.Query<AssociationView>().Where(x => x.Members.Any(y => y.AssociateId == request.AssociateId && y.Role != MembershipRole.Owner.Id));
            return Task.FromResult(associations.AsEnumerable());
        }
    }
}