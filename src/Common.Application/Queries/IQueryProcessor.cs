using System.Linq;
using Common.Application.Views;

namespace Common.Application.Queries
{
    public interface IQueryProcessor
    {
        IQueryable<T> Query<T>() where T : IView;
    }
}