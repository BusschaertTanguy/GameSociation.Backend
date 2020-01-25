using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Association.Application.Projections;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Queries.GetAssociateByAccountId
{
    public class GetAssociateByAccountIdHandler : IQueryHandler<GetAssociateByAccountId, AssociateView>
    {
        private readonly IQueryProcessor _queryProcessor;

        public GetAssociateByAccountIdHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<AssociateView> Handle(GetAssociateByAccountId request, CancellationToken cancellationToken)
        {
            var associateProjection = _queryProcessor.Query<AssociateProjection>().FirstOrDefault(x => x.AccountId == request.AccountId);
            if (associateProjection == null)
                throw new InvalidOperationException("No associate found for this account");
            var view = new AssociateView(associateProjection.Id, associateProjection.AccountId, new TagView(associateProjection.Tag.Username, associateProjection.Tag.Number));
            return Task.FromResult(view);
        }
    }
}