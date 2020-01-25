using System;
using Common.Application.Projections;

namespace Account.Application.Projections
{
    public class AccountProjection : IProjectionRoot
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}