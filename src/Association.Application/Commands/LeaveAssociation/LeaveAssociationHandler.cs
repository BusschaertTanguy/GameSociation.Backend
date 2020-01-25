using System.Threading;
using System.Threading.Tasks;
using Association.Domain.Repositories;
using Common.Application.Commands;

namespace Association.Application.Commands.LeaveAssociation
{
    public class LeaveAssociationHandler : ICommandHandler<LeaveAssociation>
    {
        private readonly IAssociationRepository _associationRepository;

        public LeaveAssociationHandler(IAssociationRepository associationRepository)
        {
            _associationRepository = associationRepository;
        }
        public async Task Handle(LeaveAssociation notification, CancellationToken cancellationToken)
        {
            var association = await _associationRepository.GetById(notification.AssociationId).ConfigureAwait(false);
            association.Leave(notification.ResponsibleId, notification.AssociateId);
            await _associationRepository.Save(association).ConfigureAwait(false);
        }
    }
}