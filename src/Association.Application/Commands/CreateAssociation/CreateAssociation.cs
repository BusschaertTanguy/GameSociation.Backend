using System;
using Common.Application.Commands;

namespace Association.Application.Commands.CreateAssociation
{
    public class CreateAssociation : ICommand
    {
        public CreateAssociation(Guid associateId, string name)
        {
            AssociateId = associateId;
            Name = name;
        }

        public Guid AssociateId { get; }
        public string Name { get; }

        public string GetQueryParameters(string prefix)
        {
            if (!string.IsNullOrEmpty(prefix))
                prefix = $"{prefix}.";

            return $"{prefix}associateId, {prefix}name";
        }
    }
}