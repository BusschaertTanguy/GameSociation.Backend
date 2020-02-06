using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Association.Application.Projections;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Queries.GetAssociation
{
    public class GetAssociationHandler : IQueryHandler<GetAssociation, AssociationDetailView>
    {
        private readonly IQueryProcessor _queryProcessor;

        public GetAssociationHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<AssociationDetailView> Handle(GetAssociation request, CancellationToken cancellationToken)
        {
            var associationProjection = _queryProcessor.Query<AssociationProjection>().FirstOrDefault(x => x.Id == request.Id);
            if (associationProjection == null)
                return Task.FromResult(default(AssociationDetailView));

            var associateIds = associationProjection.Members.Select(x => x.AssociateId).ToList();
            var associateProjections = _queryProcessor.Query<AssociateProjection>().Where(x => associateIds.Contains(x.Id)).ToList();

            var memberViews = new List<MembershipView>();

            foreach (var member in associationProjection.Members)
            {
                var associate = associateProjections.First(x => x.Id == member.AssociateId);
                var associateView = new AssociateView(associate.Id, associate.AccountId, new TagView(associate.Tag.Username, associate.Tag.Number));
                memberViews.Add(new MembershipView(member.AssociationId, associateView, member.Role, member.Status));
            }

            var associationDetailView = new AssociationDetailView(associationProjection.Id, associationProjection.Name, memberViews);
            return Task.FromResult(associationDetailView);
        }
    }
}