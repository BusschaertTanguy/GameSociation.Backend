using System;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Queries.GetAssociateByAccountId
{
    public class GetAssociateByAccountId : IQuery<AssociateView>
    {
        public GetAssociateByAccountId(Guid accountId)
        {
            AccountId = accountId;
        }

        public Guid AccountId { get; }
    }
}