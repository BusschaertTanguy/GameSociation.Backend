using System;
using Common.Application.Commands;

namespace Association.Application.Commands.KickAssociate
{
    public class KickAssociate : ICommand
    {
        public KickAssociate(Guid responsibleId, Guid associationId, Guid associateId)
        {
            ResponsibleId = responsibleId;
            AssociationId = associationId;
            AssociateId = associateId;
        }

        public Guid ResponsibleId { get; }
        public Guid AssociationId { get; }
        public Guid AssociateId { get; }
    }
}