using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            var associateView = _queryProcessor.Query<AssociateView>().FirstOrDefault(x => x.AccountId == request.AccountId);
            if (associateView == null)
                throw new InvalidOperationException("No associate found for this account");
            return Task.FromResult(associateView);
        }
    }
}