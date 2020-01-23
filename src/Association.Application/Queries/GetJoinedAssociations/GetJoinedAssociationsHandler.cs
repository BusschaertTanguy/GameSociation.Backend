using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Queries.GetJoinedAssociations
{
    public class GetJoinedAssociationsHandler : IQueryHandler<GetJoinedAssociations, List<AssociationView>>
    {
        private readonly IQueryProcessor _queryProcessor;

        public GetJoinedAssociationsHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<List<AssociationView>> Handle(GetJoinedAssociations request, CancellationToken cancellationToken)
        {
            var associationIds = _queryProcessor.Query<AssociateView>().SelectMany(x => x.JoinedAssociationIds).ToList();
            var associations = _queryProcessor.Query<AssociationView>().Where(x => associationIds.Contains(x.Id)).ToList();
            return Task.FromResult(associations);
        }
    }
}