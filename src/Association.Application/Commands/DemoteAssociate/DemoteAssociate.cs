using System;
using Common.Application.Commands;

namespace Association.Application.Commands.DemoteAssociate
{
    public class DemoteAssociate : ICommand
    {
        public DemoteAssociate(Guid responsibleId, Guid associationId, Guid associateId)
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