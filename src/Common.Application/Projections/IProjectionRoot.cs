using System;

namespace Common.Application.Projections
{
    public interface IProjectionRoot : IProjection
    {
        Guid Id { get; set; }
    }
}
