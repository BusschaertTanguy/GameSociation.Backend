using System;
using Common.Application.Commands;

namespace Association.Application.Commands.InviteToAssociation
{
    public class InviteToAssociation : ICommand
    {
        public InviteToAssociation(Guid responsibleId, Guid associationId, string username, int tagNumber)
        {
            ResponsibleId = responsibleId;
            AssociationId = associationId;
            Username = username;
            TagNumber = tagNumber;
        }

        public Guid ResponsibleId { get; }
        public Guid AssociationId { get; }
        public string Username { get; }
        public int TagNumber { get; }
    }
}