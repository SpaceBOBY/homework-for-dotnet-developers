using VRG.WarehouseGuru.Repository.Base;

namespace OrderManager.Base
{
    public interface IRepository<T> : IQueryableRepository<T>
    {
        T Insert(T entity);
    }

    public interface IRepository<T, in TKey> : IRepository<T>, IQueryableRepository<T, TKey> { }
}
