using System;
using System.Threading.Tasks;
using Common.Domain.Entities;

namespace Common.Domain.Repositories
{
    public interface IRepository<TAggregate> where TAggregate : Aggregate
    {
        Task<TAggregate> GetById(Guid id);
        Task Save(TAggregate aggregate);
    }
}