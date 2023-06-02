using OrderManager.Entities;

namespace OrderManager.Models
{
    public interface IResource<T> : IResource, IEntity<T> { }

    public interface IResource : IEntity { }
}
