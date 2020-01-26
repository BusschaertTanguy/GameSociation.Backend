using System.Threading;
using System.Threading.Tasks;
using Association.Domain.Repositories;
using Common.Application.Commands;

namespace Association.Application.Commands.DemoteAssociate
{
    public class DemoteAssociateHandler : ICommandHandler<DemoteAssociate>
    {
        private readonly IAssociationRepository _associationRepository;

        public DemoteAssociateHandler(IAssociationRepository associationRepository)
        {
            _associationRepository = associationRepository;
        }

        public async Task Handle(DemoteAssociate notification, CancellationToken cancellationToken)
        {
            var association = await _associationRepository.GetById(notification.AssociationId).ConfigureAwait(false);
            association.Demote(notification.ResponsibleId, notification.AssociateId);
            await _associationRepository.Save(association).ConfigureAwait(false);
        }
    }
}