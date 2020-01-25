using System.Threading;
using System.Threading.Tasks;
using Association.Domain.Repositories;
using Common.Application.Commands;

namespace Association.Application.Commands.RefuseInvitation
{
    public class RefuseInvitationHandler : ICommandHandler<RefuseInvitation>
    {
        private readonly IAssociationRepository _associationRepository;

        public RefuseInvitationHandler(IAssociationRepository associationRepository)
        {
            _associationRepository = associationRepository;
        }

        public async Task Handle(RefuseInvitation notification, CancellationToken cancellationToken)
        {
            var association = await _associationRepository.GetById(notification.AssociationId).ConfigureAwait(false);
            association.RefuseInvitation(notification.ResponsibleId, notification.AssociateId);
            await _associationRepository.Save(association);
        }
    }
}