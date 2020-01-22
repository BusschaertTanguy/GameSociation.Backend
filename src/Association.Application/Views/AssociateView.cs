using System;
using Common.Application.Views;

namespace Association.Application.Views
{
    public class AssociateView : IView
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public TagView Tag { get; set; }
    }
}