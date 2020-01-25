using System.Linq;
using Common.Application.Projections;

namespace Common.Application.Queries
{
    public interface IQueryProcessor
    {
        IQueryable<T> Query<T>() where T : IProjectionRoot;
    }
}