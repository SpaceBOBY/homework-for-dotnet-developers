using System.Linq;
using System.Threading.Tasks;

namespace VRG.WarehouseGuru.Repository.Base
{
    public interface IQueryableRepository<out T>
    {
        IQueryable<T> Get();
    }

    public interface IQueryableRepository<out T, in TKey> : IQueryableRepository<T>
    {
        IQueryable<T> GetById(TKey id);
        Task<bool> ExistsAsync(TKey id);
    }
}
