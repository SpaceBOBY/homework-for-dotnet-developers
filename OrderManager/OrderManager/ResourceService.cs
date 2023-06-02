using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManager.Base;
using OrderManager.Entities;
using OrderManager.Extensions;
using OrderManager.Models;

namespace OrderManager
{
    public abstract class ResourceService<T, TResource, TKey>
            where T : class, IEntity<TKey>
            where TResource : IResource<TKey>
    {
        protected readonly IMapper Mapper;
        protected readonly DbContext Context;

        public abstract IRepository<T, TKey> Repository { get; }

        public ResourceService(DbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public virtual async Task<TResource> CreateAsync<TForm>(TForm form)
        {
            var entity = Mapper.Map<T>(form);
            entity = await InsertAsync(entity);
            await Context.SaveChangesAsync();
            var result = Mapper.Map<TResource>(entity);
            return result;
        }

        protected virtual ValueTask<T> InsertAsync(T entity)
        {
            entity = Repository.Insert(entity);
            return new(entity);
        }
    }
}
