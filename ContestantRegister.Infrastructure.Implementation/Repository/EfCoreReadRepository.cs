using System.Linq;
using System.Threading.Tasks;
using ContestantRegister.DataAccess;
using ContestantRegister.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContestantRegister.Infrastructure.Implementation
{
    public class EfCoreReadRepository : IReadRepository
    {
        protected readonly ApplicationDbContext Context;

        public EfCoreReadRepository(ApplicationDbContext context)
        {
            Context = context;
        }
        public void Dispose()
        {
            Context.Dispose();
        }

        public ValueTask<TEntity> FindAsync<TEntity>(object key) where TEntity : class
        {
            return Context.FindAsync<TEntity>(key);
        }

        public virtual IQueryable<TEntity> Set<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>().AsNoTracking();
        }
    }
}
