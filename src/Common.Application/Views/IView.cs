using System;

namespace Common.Application.Views
{
    public interface IView : IProjection
    {
        Guid Id { get; set; }
    }
}
