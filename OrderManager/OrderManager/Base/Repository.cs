using Microsoft.EntityFrameworkCore;
using OrderManager.Entities;

namespace OrderManager.Base
{
    public class Repository
    {
        protected readonly DbContext Context;

        protected Repository(DbContext context)
        {
            Context = context;
        }
    }

    public class Repository<T> : Repository, IRepository<T> where T : class, IEntity
    {
        protected readonly DbSet<T> Set;

        protected Repository(DbContext context) : base(context)
        {
            Set = context.Set<T>();
        }

        public virtual IQueryable<T> Get() => Set;

        public virtual T Insert(T entity) => Set.Add(entity).Entity;

        public virtual void Insert(IEnumerable<T> entities) => Set.AddRange(entities);
    }
}
