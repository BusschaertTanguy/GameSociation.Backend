using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Association.Application.Views;
using Association.Domain.Repositories;
using Common.Application.Commands;
using Common.Application.Queries;

namespace Association.Application.Commands.InviteToAssociation
{
    public class InviteToAssociationHandler : ICommandHandler<InviteToAssociation>
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAssociationRepository _associationRepository;

        public InviteToAssociationHandler(IQueryProcessor queryProcessor, IAssociationRepository associationRepository)
        {
            _queryProcessor = queryProcessor;
            _associationRepository = associationRepository;
        }

        public async Task Handle(InviteToAssociation notification, CancellationToken cancellationToken)
        {
            var associateView = _queryProcessor.Query<AssociateView>().FirstOrDefault(x => x.Tag.Username == notification.Username && x.Tag.Number == notification.TagNumber);
            if (associateView == null)
                throw new InvalidOperationException("No associate found with that tag.");

            var association = await _associationRepository.GetById(notification.AssociationId).ConfigureAwait(false);
            association.Invite(notification.ResponsibleId, associateView.Id);
            await _associationRepository.Save(association).ConfigureAwait(false);
        }
    }
}