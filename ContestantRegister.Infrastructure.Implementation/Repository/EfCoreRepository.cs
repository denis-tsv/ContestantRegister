using System;
using System.Linq;
using System.Threading.Tasks;
using ContestantRegister.DataAccess;
using ContestantRegister.Domain;
using ContestantRegister.Domain.Repository;
using ContestantRegister.Models;

namespace ContestantRegister.Infrastructure.Implementation
{
    public class EfCoreRepository : EfCoreReadRepository, IRepository
    {
        public EfCoreRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Add<TEntity>(TEntity entity) 
        {
            Context.Add(entity);
        }

        public void Remove<TEntity>(TEntity entity)
        {
            Context.Remove(entity);
        }

        public void RemoveById<TEntity, TKey>(TKey id) 
            where TEntity : class, IHasId<TKey>, new()
            where TKey : IEquatable<TKey>
        {
            var entity = new TEntity {Id = id};
            Context.Remove<TEntity>(entity);
        }

        public void Update<TEntity>(TEntity entity)
        {
            Context.Update(entity);
        }

        public override IQueryable<TEntity> Set<TEntity>() 
        {
            //включаем changetracking при выборке 
            return Context.Set<TEntity>();
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
