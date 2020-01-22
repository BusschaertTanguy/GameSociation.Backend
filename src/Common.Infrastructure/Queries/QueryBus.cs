using System.Threading.Tasks;
using Common.Application.Queries;
using MediatR;

namespace Common.Infrastructure.Queries
{
    public class QueryBus : IQueryBus
    {
        private readonly IMediator _mediator;

        public QueryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResult> Process<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            return await _mediator.Send(query).ConfigureAwait(false);
        }
    }
}