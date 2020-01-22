using MediatR;

namespace Common.Application.Queries
{
    public interface IQuery<out T> : IRequest<T>
    {
    }
}