using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Association.Application.Projections;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Queries.GetAssociateAssociations
{
    public class GetAssociateAssociationsHandler : IQueryHandler<GetAssociateAssociations, List<AssociationView>>
    {
        private readonly IQueryProcessor _queryProcessor;

        public GetAssociateAssociationsHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<List<AssociationView>> Handle(GetAssociateAssociations request, CancellationToken cancellationToken)
        {
            var associateProjection = _queryProcessor.Query<AssociateProjection>().FirstOrDefault(x => x.Id == request.AssociateId);
            if (associateProjection == null)
                return Task.FromResult(new List<AssociationView>());

            var associationIds = new List<Guid>();

            if (associateProjection.JoinedAssociationIds != null)
                associationIds.AddRange(associateProjection.JoinedAssociationIds);

            if (associateProjection.OwnedAssociationIds != null)
                associationIds.AddRange(associateProjection.OwnedAssociationIds);

            var associationProjections = _queryProcessor.Query<AssociationProjection>().Where(x => associationIds.Contains(x.Id)).ToList();
            var associationViews = associationProjections.Select(x => new AssociationView(x.Id, x.Name)).ToList();

            return Task.FromResult(associationViews);
        }
    }
}