using System;
using System.Collections.Generic;
using Common.Application.Views;

namespace Association.Application.Views
{
    public class AssociationView : IView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<MembershipView> Members { get; set; }
    }
}