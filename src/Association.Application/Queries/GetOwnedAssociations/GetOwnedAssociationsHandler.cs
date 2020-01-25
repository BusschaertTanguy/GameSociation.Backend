using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Association.Application.Projections;
using Association.Application.Views;
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
            var associateProjection = _queryProcessor.Query<AssociateProjection>().FirstOrDefault(x => x.Id == request.AssociateId);
            if (associateProjection?.OwnedAssociationIds == null)
                return Task.FromResult(new List<AssociationView>());


            var associationProjections = _queryProcessor
                .Query<AssociationProjection>()
                .Where(x => associateProjection.OwnedAssociationIds.Contains(x.Id))
                .ToList();

            var associationViews = associationProjections.Select(x => new AssociationView(x.Id, x.Name)).ToList();
            return Task.FromResult(associationViews);
        }
    }
}