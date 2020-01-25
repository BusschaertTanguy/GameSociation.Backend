using System.Threading;
using System.Threading.Tasks;
using Association.Domain.Repositories;
using Common.Application.Commands;

namespace Association.Application.Commands.AcceptInvitation
{
    public class AcceptInvitationHandler : ICommandHandler<AcceptInvitation>
    {
        private readonly IAssociationRepository _associationRepository;

        public AcceptInvitationHandler(IAssociationRepository associationRepository)
        {
            _associationRepository = associationRepository;
        }

        public async Task Handle(AcceptInvitation notification, CancellationToken cancellationToken)
        {
            var association = await _associationRepository.GetById(notification.AssociationId).ConfigureAwait(false);
            association.AcceptInvitation(notification.ResponsibleId, notification.AssociateId);
            await _associationRepository.Save(association).ConfigureAwait(false);
        }
    }
}