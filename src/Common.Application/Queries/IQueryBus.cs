using System.Threading.Tasks;

namespace Common.Application.Queries
{
    public interface IQueryBus
    {
        public Task<TResult> Process<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}