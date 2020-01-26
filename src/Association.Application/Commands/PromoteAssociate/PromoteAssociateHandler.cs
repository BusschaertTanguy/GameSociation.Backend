using System;
using System.Threading;
using System.Threading.Tasks;
using Association.Domain.Repositories;
using Common.Application.Commands;

namespace Association.Application.Commands.PromoteAssociate
{
    public class PromoteAssociateHandler : ICommandHandler<PromoteAssociate>
    {
        private readonly IAssociationRepository _associationRepository;

        public PromoteAssociateHandler(IAssociationRepository associationRepository)
        {
            _associationRepository = associationRepository;
        }

        public async Task Handle(PromoteAssociate notification, CancellationToken cancellationToken)
        {
            var association = await _associationRepository.GetById(notification.AssociationId).ConfigureAwait(false);
            association.Promote(notification.ResponsibleId, notification.AssociateId);
            await _associationRepository.Save(association).ConfigureAwait(false);
        }
    }
}