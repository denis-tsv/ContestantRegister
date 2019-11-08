using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContestantRegister.Domain.Repository
{
    internal interface IReadRepository : IDisposable
    {
        ValueTask<TEntity> FindAsync<TEntity>(object key) where TEntity : class;

        IQueryable<TEntity> Set<TEntity>() where TEntity : class;
    }
}
