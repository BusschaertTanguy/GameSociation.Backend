﻿using System;
using Common.Domain.Events;

namespace Association.Domain.Events
{
    public class InvitationRefused : IEvent
    {
        public InvitationRefused(Guid associationId, Guid associateId)
        {
            Id = associationId;
            AssociateId = associateId;
        }

        public Guid AssociateId { get; }
        public Guid Id { get; }
    }
}