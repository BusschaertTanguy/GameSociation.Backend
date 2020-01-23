using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Association.Application.Views;
using Association.Domain.Enumerations;
using Common.Application.Queries;

namespace Association.Application.Queries.GetOwnedAssociations
{
    public class GetOwnedAssociationsHandler : IQueryHandler<GetOwnedAssociations, List<AssociationView>>
    {
        private readonly IQueryProcessor _queryProcessor;

        public GetOwnedAssociationsHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<List<AssociationView>> Handle(GetOwnedAssociations request, CancellationToken cancellationToken)
        {
            var associationIds = _queryProcessor.Query<AssociateView>().SelectMany(x => x.OwnedAssociationIds).ToList();
            var associations = _queryProcessor.Query<AssociationView>().Where(x => associationIds.Contains(x.Id)).ToList();
            return Task.FromResult(associations);
        }
    }
}