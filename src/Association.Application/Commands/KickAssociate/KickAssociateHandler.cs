using System.Threading;
using System.Threading.Tasks;
using Association.Domain.Repositories;
using Common.Application.Commands;

namespace Association.Application.Commands.KickAssociate
{
    public class KickAssociateHandler : ICommandHandler<KickAssociate>
    {
        private readonly IAssociationRepository _associationRepository;

        public KickAssociateHandler(IAssociationRepository associationRepository)
        {
            _associationRepository = associationRepository;
        }

        public async Task Handle(KickAssociate notification, CancellationToken cancellationToken)
        {
            var association = await _associationRepository.GetById(notification.AssociationId).ConfigureAwait(false);
            association.Kick(notification.ResponsibleId, notification.AssociateId);
            await _associationRepository.Save(association).ConfigureAwait(false);
        }
    }
}