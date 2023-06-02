using AutoMapper;
using AutoMapper.Internal;
using AutoMapper.QueryableExtensions;
using OrderManager.Entities;
using System.Linq.Expressions;

namespace OrderManager.Extensions
{
    public static class MapperExtensions
    {
        public static IQueryable<T> ProjectTo<T>(this IQueryable source, IMapper mapper, params Expression<Func<T, object>>[] membersToExpand)
        {
            return source.ProjectTo(mapper.ConfigurationProvider, null, membersToExpand);
        }

        public static void Update<T1, T2>(this IMapper mapper, T2 target, T1 source)
        {
            var result = mapper.Map(source, target);

            // AssignableMapper returns source so we assume not having mapping defined
            if (ReferenceEquals(source, result))
            {
                var pair = new TypePair(target.GetType(), source?.GetType());
                throw new AutoMapperMappingException("No mapping defined for the patching!", null, pair);
            }
        }

        public static void Patch<TKey, TSource>(this IMapper mapper, IEntity<TKey> target, TSource source)
        {
            var id = target.Id;
            mapper.Update(target, source);
            target.Id = id;
        }
    }
}
