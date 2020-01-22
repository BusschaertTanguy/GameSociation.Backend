using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Queries.GetAssociation
{
    public class GetAssociationHandler : IQueryHandler<GetAssociation, AssociationView>
    {
        private readonly IQueryProcessor _queryProcessor;

        public GetAssociationHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<AssociationView> Handle(GetAssociation request, CancellationToken cancellationToken)
        {
            var associationView = _queryProcessor.Query<AssociationView>().FirstOrDefault(x => x.Id == request.Id);
            return Task.FromResult(associationView);
        }
    }
}