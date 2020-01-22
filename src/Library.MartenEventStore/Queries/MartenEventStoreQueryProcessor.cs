using System.Linq;
using Common.Application.Queries;
using Common.Application.Views;
using Marten;

namespace Library.MartenEventStore.Queries
{
    public class MartenEventStoreQueryProcessor : IQueryProcessor
    {
        private readonly IQuerySession _session;

        public MartenEventStoreQueryProcessor(IQuerySession session)
        {
            _session = session;
        }


        public IQueryable<T> Query<T>() where T : IView
        {
            var queryable = _session.Query<T>();
            return queryable;
        }
    }
}